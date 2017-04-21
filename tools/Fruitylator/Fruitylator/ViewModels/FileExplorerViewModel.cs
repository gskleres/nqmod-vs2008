using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(FileExplorerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileExplorerViewModel : Screen
    {

    }
}