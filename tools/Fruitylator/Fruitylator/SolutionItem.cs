using System;
using System.IO;
using System.Linq;
using Caliburn.Micro;
using Fruitylator.Core;
using Fruitylator.Core.Interfaces;
using Fruitylator.Styles;

namespace Fruitylator
{
    public enum SolutionItemType
    {
        Solution,
        Folder,
        File
    }

    public class SolutionItem
    {
        public SolutionItem(string rootDir, SolutionItem root, SolutionItemType type)
        {
            Root = root ?? this;
            Path = rootDir;
            DisplayName = rootDir.Split('\\').Last();
            Type = type;
            switch (type)
            {
                case SolutionItemType.Solution:
                    Icon = Fa.Book;
                    break;
                case SolutionItemType.Folder:
                    Icon = Fa.FolderOpenO;
                    break;
                case SolutionItemType.File:
                    Icon = Fa.FileCodeO;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public SolutionItem Root { get; }

        public string Path { get; }
        
        public SolutionItemType Type { get; }

        public string Icon { get; }

        public ITranslatable Translatable { get; private set; }

        public string DisplayName { get; set; }

        public BindableCollection<SolutionItem> Children { get; } = new BindableCollection<SolutionItem>();

        public void Load()
        {
            var directories = Directory
                .GetDirectories(Path, "*", SearchOption.TopDirectoryOnly)
                .Select(dirPath => new SolutionItem(dirPath, Root, SolutionItemType.Folder))
                .ToList();

            var files = Directory
                .GetFiles(Path, "*.xml", SearchOption.TopDirectoryOnly)
                .Select(TranslatableFile.OpenRead)
                .Where(file => file.Translatable != null)
                .Select(file => new SolutionItem(file.OriginalFilePath, Root, SolutionItemType.File)
                {
                    Translatable = file.Translatable
                })
                .ToList();

            foreach (var directory in directories)
                directory.Load();

            Children.AddRange(directories.OrderBy(d => d.DisplayName));
            Children.AddRange(files.OrderBy(f => f.DisplayName));
        }

        public override string ToString() => DisplayName;
    }
}
