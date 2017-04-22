using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(ShellViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Screen
    {
        private FileExplorerViewModel _fileExplorerViewModel;
        private TabViewModel _tabViewModel;

        [ImportingConstructor]
        public ShellViewModel(FileExplorerViewModel fileExplorerViewModel, TabViewModel tabViewModel)
        {
            FileExplorerViewModel = fileExplorerViewModel;
            TabViewModel = tabViewModel;
        }

        public override string DisplayName
        {
            get { return "Fruitylator"; }
            set { }
        }

        public FileExplorerViewModel FileExplorerViewModel
        {
            get { return _fileExplorerViewModel; }
            set
            {
                _fileExplorerViewModel = value;
                NotifyOfPropertyChange();
            }
        }

        public TabViewModel TabViewModel
        {
            get { return _tabViewModel; }
            set
            {
                _tabViewModel = value;
                NotifyOfPropertyChange();
            }
        }
    }
}