using System.Collections.Generic;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    internal class TranslatableFile : ITranslatableFile
    {
        public string FilePath { get; }
        
        public string XmlContent { get; }

        public IEnumerable<ITranslatablePart> Parts { get; }

        public TranslatableFile(string filePath, string xmlContent, IEnumerable<ITranslatablePart> parts)
        {
            FilePath = filePath;
            XmlContent = xmlContent;
            Parts = parts;
        }
    }
}
