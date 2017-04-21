using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(ShellViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : Conductor<Screen>
    {
        private FileExplorerViewModel _fileExplorerViewModel;

        public FileExplorerViewModel FileExplorerViewModel
        {
            get { return _fileExplorerViewModel; }
            set
            {
                _fileExplorerViewModel = value;
                NotifyOfPropertyChange();
            }
        }

        [ImportingConstructor]
        public ShellViewModel(FileExplorerViewModel fileExplorerViewModel)
        {
            FileExplorerViewModel = fileExplorerViewModel;
            base.ActivateItem(IoC.Get<EditorViewModel>());
        }
    }
}
