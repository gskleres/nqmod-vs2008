using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(BrowserViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BrowserViewModel : Screen
    {
        public SolutionItem Root { get; private set; }

        public SearchViewModel Search { get; }

        [ImportingConstructor]
        public BrowserViewModel(SearchViewModel search)
        {
            Search = search;
        }

        public void LoadSolution(SolutionItem item)
        {
            Root = item.Root;
            Search.Load(item.Root);
        }
    }
}