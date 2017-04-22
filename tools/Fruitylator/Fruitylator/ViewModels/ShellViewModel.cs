using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(ShellViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Screen
    {
        public ExplorerViewModel Explorer { get; }

        public BrowserConductorViewModel BrowserConductor { get; }

        [ImportingConstructor]
        public ShellViewModel(
            ExplorerViewModel explorer,
            BrowserConductorViewModel browserConductor)
        {
            Explorer = explorer;
            BrowserConductor = browserConductor;
        }

        public override string DisplayName
        {
            get { return "Fruitylator"; }
            set { }
        }
    }
}