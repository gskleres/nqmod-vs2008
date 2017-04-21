using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Caliburn.Micro;
using Fruitylator.Core;
using Fruitylator.Core.Interfaces;
using Fruitylator.Events;
using Screen = Caliburn.Micro.Screen;

namespace Fruitylator.ViewModels
{
    [Export(typeof(FileExplorerViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileExplorerViewModel : Screen
    {
        private readonly IXmlTransformer _transformer;

        private readonly IEventAggregator _events;

        private string _selectedFile;

        private readonly string _defaultPath;

        private string _filePath;

        public BindableCollection<ITranslatableContent> Files { get; } = new BindableCollection<ITranslatableContent>();

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                NotifyOfPropertyChange();
            }
        }

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
            _transformer = transformer;
            _events = events;
            _defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FilePath = _defaultPath;
        }

        public void SelectFilePath()
        {
            var selectedPath = Directory.Exists(_filePath) ? _filePath : _defaultPath;
            using (var dialog = new FolderBrowserDialog { SelectedPath = selectedPath })
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    FilePath = dialog.SelectedPath;
                }
            }
        }

        public void SearchTranslatableFiles()
        {
            var xmlFiles = Directory.GetFiles(_filePath, "*.xml", SearchOption.AllDirectories);
            Files.Clear();
            Files.AddRange(xmlFiles.Select(_transformer.LoadFile).Where(t => t != null && t.Parts.Any()));

        }
    }
}