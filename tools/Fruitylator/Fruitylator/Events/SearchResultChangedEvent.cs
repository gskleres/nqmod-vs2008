using Fruitylator.Core.Interfaces;

namespace Fruitylator.Events
{
    public class SearchResultChangedEvent
    {
        public ITranslatablePart SelectedResult { get; set; }

        public SearchResultChangedEvent(ITranslatablePart part)
        {
            SelectedResult = part;
        }
    }
}
