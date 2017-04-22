using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Caliburn.Micro;
using Fruitylator.Core.Interfaces;
using Fruitylator.Events;
using Screen = Caliburn.Micro.Screen;

namespace Fruitylator.ViewModels
{
    [Export(typeof(FileExplorerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileExplorerViewModel : Screen
    {
        private readonly string _defaultPath;

        private readonly IEventAggregator _events;
        private readonly IXmlTransformer _transformer;

        private string _filePath;

        private ITranslatableFile _selectedTranslatable;

        [ImportingConstructor]
        public FileExplorerViewModel(IXmlTransformer transformer, IEventAggregator events)
        {
            _transformer = transformer;
            _events = events;
            _defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Repos",
                "nqmod-vs2008");
            FilePath = _defaultPath;
        }

        public BindableCollection<ITranslatableFile> Files { get; } = new BindableCollection<ITranslatableFile>();

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange();
            }
        }

        public ITranslatableFile SelectedTranslatable
        {
            get { return _selectedTranslatable; }
            set
            {
                _selectedTranslatable = value;
                _events.PublishOnUIThread(new FileChangedEvent(_selectedTranslatable));
                NotifyOfPropertyChange();
            }
        }

        public void SelectFilePath()
        {
            var selectedPath = Directory.Exists(_filePath) ? _filePath : _defaultPath;
            using (var dialog = new FolderBrowserDialog {SelectedPath = selectedPath})
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    FilePath = dialog.SelectedPath;
            }
        }

        public void SearchTranslatableFiles()
        {
            Files.Clear();
            if (!Directory.Exists(_filePath)) return;
            var xmlFiles = Directory.GetFiles(_filePath, "*.xml", SearchOption.AllDirectories);
            Files.AddRange(xmlFiles.Select(_transformer.LoadFile).Where(t => t != null && t.Parts.Any()));
        }
    }
}