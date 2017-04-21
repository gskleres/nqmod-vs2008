using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core;
using Fruitylator.Core.Interfaces;
using Fruitylator.Events;

namespace Fruitylator.ViewModels
{
    [Export(typeof(FileExplorerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileExplorerViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private string _selectedFile;
        public BindableCollection<ITranslatableContent> Files { get; private set; }

        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                _events.PublishOnUIThread(new FileChangedEvent(_selectedFile));
                NotifyOfPropertyChange();
            }
        }

        [ImportingConstructor]
        public FileExplorerViewModel(IXmlTransformer transformer, IEventAggregator events)
        {
            _events = events;
            const string dirPath = @"C:\Users\Michael\Documents\Repos\nqmod-vs2008";
            var xmlFiles = Directory.GetFiles(dirPath, "*.xml", SearchOption.AllDirectories);
            Files =
                new BindableCollection<ITranslatableContent>(xmlFiles.Select(transformer.LoadFile).Where(t => t != null && t.Parts.Any()));
        }
    }
}