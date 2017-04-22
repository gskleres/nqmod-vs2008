using Fruitylator.Core.Interfaces;

namespace Fruitylator.Events
{
    public class SolutionChangedEvent
    {
        public SolutionItem SolutionItem { get; private set; }

        public SolutionChangedEvent(SolutionItem solutionItem)
        {
            SolutionItem = solutionItem;
        }
    }
}
