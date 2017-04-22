using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.ViewModels
{
    [Export(typeof(RawEditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RawEditorViewModel : Screen
    {
        private ITranslatablePart _part;

        public ITranslatablePart Part
        {
            get { return _part; }
            set
            {
                _part = value;
                NotifyOfPropertyChange();
            }
        }

        public RawEditorViewModel()
        {
            DisplayName = "Raw";
        }

        public void LoadTranslatable(ITranslatablePart part)
        {
            Part = part;
        }
    }
}