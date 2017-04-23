using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Caliburn.Micro;
using Fruitylator.Events;
using Fruitylator.Styles;
using Screen = Caliburn.Micro.Screen;

namespace Fruitylator.ViewModels
{
    [Export(typeof(ExplorerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExplorerViewModel : Screen
    {
        private readonly string _defaultPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Repos",
                "nqmod-vs2008",
                "FruityMod");

        [Import] private IEventAggregator _events;

        private readonly Dictionary<string, SolutionItem> _rootsDict = new Dictionary<string, SolutionItem>();

        public BindableCollection<SolutionItem> SolutionTree { get; } = new BindableCollection<SolutionItem>();

        public void SelectedItemChanged(RoutedPropertyChangedEventArgs<object> args)
        {
            SelectItem((SolutionItem) args.NewValue);
        }

        private void SelectItem(SolutionItem value)
        {
            _events.PublishOnUIThread(new SolutionItemChangedEvent(value));
        }

        public void OpenFolder()
        {
            var selectedPath = Directory.Exists(_defaultPath)
                ? _defaultPath
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (var dialog = new FolderBrowserDialog {SelectedPath = selectedPath})
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    selectedPath = dialog.SelectedPath;
            }
            if (!_rootsDict.ContainsKey(selectedPath))
            {
                _rootsDict[selectedPath] = new SolutionItem(selectedPath, null, SolutionItemType.Solution)
                {
                    DisplayName = selectedPath,
                };
                _rootsDict[selectedPath].Load();
                SolutionTree.Add(_rootsDict[selectedPath]);
            }
            SelectItem(_rootsDict[selectedPath]);
        }
    }
}