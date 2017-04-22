using System.ComponentModel.Composition;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.ViewModels
{
    [Export(typeof(SearchDetailsViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SearchDetailsViewModel :
        Conductor<Screen>.Collection.OneActive
    {
        private readonly RawEditorViewModel _rawEditor;
        private readonly FileEditorViewModel _fileEditor;

        [ImportingConstructor]
        public SearchDetailsViewModel(
            RawEditorViewModel rawEditor,
            FileEditorViewModel fileEditor)
        {
            _rawEditor = rawEditor;
            _fileEditor = fileEditor;
            Items.Add(rawEditor);
            Items.Add(fileEditor);
        }

        public void LoadTranslatablePart(ITranslatablePart part)
        {
            Part = part;
            _rawEditor.LoadTranslatable(part);
            _fileEditor.LoadFile(part.OriginalFilePath);
        }

        public ITranslatablePart Part { get; private set; }
    }
}