using System;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(BrowserConductorViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BrowserConductorViewModel :
        Conductor<BrowserViewModel>.Collection.OneActive,
        IHandle<SolutionItemChangedEvent>
    {
        private readonly Func<SolutionItem, BrowserViewModel> _browserFactory;

        [ImportingConstructor]
        public BrowserConductorViewModel(
            IEventAggregator events,
            Func<SolutionItem, BrowserViewModel> browserFactory)
        {
            _browserFactory = browserFactory;
            events.Subscribe(this);
        }

        public void Handle(SolutionItemChangedEvent message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var alreadyOpened = Items.FirstOrDefault(vm => vm.Root == message.SolutionItem.Root);

            if (alreadyOpened != null)
            {
                ActivateItem(alreadyOpened);
                return;
            }

            var solutionBrowser = _browserFactory(message.SolutionItem);
            ActivateItem(solutionBrowser);
        }
    }
}
