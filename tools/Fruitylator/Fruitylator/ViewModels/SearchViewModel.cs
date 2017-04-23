using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(SearchViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchViewModel :
        Conductor<SearchDetailsViewModel>.Collection.OneActive,
        IHandle<SearchResultChangedEvent>,
        IHandle<SolutionItemChangedEvent>

    {
        private readonly Func<ITranslatablePart, SearchDetailsViewModel> _searchDetailsFactory;

        private readonly List<ITranslatablePart> _duplicateData = new List<ITranslatablePart>();

        private readonly Dictionary<(string tag, string lang, string file), ITranslatablePart> _translatableData =
            new Dictionary<(string tag, string lang, string file), ITranslatablePart>();

        private List<ITranslatablePart> _excludedResults = new List<ITranslatablePart>();
        private string _searchText;
        private string _selectedLanguagePattern;
        private bool _hideDuplicates = true;
        private ITranslatablePart _selectedResult;
        private string _selectedFilePath;

        public BindableCollection<string> LanguagePatterns { get; } =
            new BindableCollection<string>();

        public ITranslatablePart SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                NotifyOfPropertyChange();
                Handle(new SearchResultChangedEvent(_selectedResult));
            }
        }

        public bool HideDuplicates
        {
            get { return _hideDuplicates; }
            set
            {
                _hideDuplicates = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => ResultsInfo);
            }
        }

        public string SelectedLanguagePattern
        {
            get { return _selectedLanguagePattern; }
            set
            {
                _selectedLanguagePattern = value;
                NotifyOfPropertyChange();
                Search(_searchText, _selectedLanguagePattern, _selectedFilePath);
            }
        }

        public string ResultsInfo => _hideDuplicates
            ? $"{Results.Count} results"
            : $"{Results.Count} results, {_duplicateData.Count} duplicates";

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                Search(_searchText, _selectedLanguagePattern, _selectedFilePath);
                NotifyOfPropertyChange();
            }
        }

        public BindableCollection<ITranslatablePart> Results { get; } = new BindableCollection<ITranslatablePart>();

        [ImportingConstructor]
        public SearchViewModel(
            IEventAggregator events,
            Func<ITranslatablePart, SearchDetailsViewModel> searchDetailsFactory)
        {
            _searchDetailsFactory = searchDetailsFactory;
            events.Subscribe(this);
        }

        private void Search(string searchText, string languagePattern, string filePath)
        {
            if (searchText == null) searchText = string.Empty;
            if (languagePattern == null) languagePattern = string.Empty;
            if (filePath == null) filePath = string.Empty;

            var patterns = searchText
                .ToLower()
                .Split(' ')
                .Where(p => p.Length > 0)
                .Select(p => p.Trim());

            var filtered = _translatableData.Values
                .Where(r =>
                    r.Language.ToLower().StartsWith(languagePattern.ToLower()) &&
                    r.OriginalFilePath.ToLower().Contains(filePath.ToLower()) &&
                    patterns
                        .All(p =>
                            r.Language.ToLower().Contains(p) ||
                            r.Tag.ToLower().Contains(p) ||
                            r.OriginalFilePath.ToLower().Contains(p) ||
                            r.Text.Split(' ').Any(t => t.ToLower().Contains(p))))
                .ToList();

            _excludedResults = _translatableData.Values
                .Where(r => !filtered.Contains(r))
                .ToList();

            Results.RemoveRange(_excludedResults);
            var newResults = filtered.Where(f => !Results.Contains(f));
            Results.AddRange(newResults);
            NotifyOfPropertyChange(() => ResultsInfo);
        }

        public void Load(SolutionItem root)
        {
            Clear();
            LoadSolutionItem(root);
            Results.AddRange(_translatableData.Values);
            NotifyOfPropertyChange(() => ResultsInfo);
        }

        private void LoadSolutionItem(SolutionItem item)
        {
            foreach (
                var tlPart in item
                    .Children
                    .Where(i => i.Type == SolutionItemType.File)
                    .SelectMany(i => i.Translatable.Parts))
            {
                if (_translatableData.Keys.Any(k => k.tag == tlPart.Tag && k.lang == tlPart.Language))
                    _duplicateData.Add(tlPart);

                if (!LanguagePatterns.Contains(tlPart.Language))
                    LanguagePatterns.Add(tlPart.Language);

                _translatableData.Add((tlPart.Tag, tlPart.Language, tlPart.OriginalFilePath), tlPart);
            }

            foreach (
                var folder in item
                    .Children
                    .Where(i => i.Type == SolutionItemType.Folder))
                LoadSolutionItem(folder);
        }

        private void Clear()
        {
            _translatableData.Clear();
            _duplicateData.Clear();

            _excludedResults.Clear();
            Results.Clear();

            SearchText = string.Empty;

            LanguagePatterns.Clear();
            LanguagePatterns.Add(string.Empty);
            SelectedLanguagePattern = LanguagePatterns.First();
            NotifyOfPropertyChange(() => ResultsInfo);
        }

        public void Handle(SearchResultChangedEvent message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (message.SelectedResult == null)
            {
                ActivateItem(null);
                return;
            }

            var alreadyOpened = Items.FirstOrDefault(vm => vm.Part == message.SelectedResult);
            if (alreadyOpened != null)
            {
                ActivateItem(alreadyOpened);
                return;
            }

            var solutionBrowser = _searchDetailsFactory(message.SelectedResult);
            ActivateItem(solutionBrowser);
        }

        public void Handle(SolutionItemChangedEvent message)
        {
            _selectedFilePath = message.SolutionItem.Path;
            Search(_searchText, _selectedLanguagePattern, _selectedFilePath);
        }
    }
}