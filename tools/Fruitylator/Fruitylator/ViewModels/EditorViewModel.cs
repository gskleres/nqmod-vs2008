using System.ComponentModel.Composition;
using Caliburn.Micro;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(EditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditorViewModel : Screen, IHandle<FileChangedEvent>
    {
        [ImportingConstructor]
        public EditorViewModel(IEventAggregator events)
        {
            events.Subscribe(this);
        }

        public void Handle(FileChangedEvent message)
        {

        }
    }
}