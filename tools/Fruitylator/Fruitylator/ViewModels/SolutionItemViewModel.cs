using System;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(SolutionItemViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SolutionItemViewModel : Conductor<RawEditorViewModel>.Collection.OneActive, IHandle<SolutionChangedEvent>
    {
        private readonly Func<SolutionItem,RawEditorViewModel> _editorFactory;

        [ImportingConstructor]
        public SolutionItemViewModel(IEventAggregator events, Func<SolutionItem, RawEditorViewModel> editorFactory)
        {
            _editorFactory = editorFactory;
            events.Subscribe(this);
        }
 
        public void Handle(SolutionChangedEvent message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            if (message.SolutionItem?.Translatable == null)
                // directory or null
                return;

            var alreadyOpened = Items.FirstOrDefault(vm =>
            {
                var editorFilePath = vm.Part.OriginalFilePath;
                return !string.IsNullOrWhiteSpace(editorFilePath) &&
                       editorFilePath == message.SolutionItem.Translatable.OriginalFilePath;
            });

            if (alreadyOpened != null)
            {
                ActivateItem(alreadyOpened);
                return;
            }

            var editor = _editorFactory(message.SolutionItem);
            ActivateItem(editor);
        }
    }
}