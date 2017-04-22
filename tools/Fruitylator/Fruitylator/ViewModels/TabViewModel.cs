using System;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(TabViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TabViewModel : Conductor<EditorViewModel>.Collection.OneActive, IHandle<FileChangedEvent>
    {
        private readonly Func<ITranslatableFile, EditorViewModel> _editorFactory;

        [ImportingConstructor]
        public TabViewModel(IEventAggregator events, Func<ITranslatableFile, EditorViewModel> editorFactory)
        {
            _editorFactory = editorFactory;
            events.Subscribe(this);
        }
 
        public void Handle(FileChangedEvent message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
 
            var alreadyOpened = Items.FirstOrDefault(i =>
            {
                var filePath = i.Translatable.FilePath;
                return !string.IsNullOrWhiteSpace(filePath) && filePath == message.Translatable.FilePath;
            });
            if (alreadyOpened != null)
            {
                ActivateItem(alreadyOpened);
                return;
            }
            var editor = _editorFactory(message.Translatable);
            ActivateItem(editor);
        }
    }
}