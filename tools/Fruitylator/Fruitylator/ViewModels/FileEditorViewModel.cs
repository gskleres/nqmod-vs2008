using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Fruitylator.ViewModels
{
    [Export(typeof(FileEditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileEditorViewModel : Screen
    {
        public FileEditorViewModel()
        {
            DisplayName = "File";
        }

        public void LoadFile(string filePath)
        {
        }
    }
}