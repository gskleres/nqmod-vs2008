using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(EditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditorViewModel : Screen
    {
    }
}