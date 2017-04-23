using Fruitylator.Core.Interfaces;

namespace Fruitylator.Events
{
    public class SolutionItemChangedEvent
    {
        public SolutionItem SolutionItem { get; private set; }

        public SolutionItemChangedEvent(SolutionItem solutionItem)
        {
            SolutionItem = solutionItem;
        }
    }
}
