using System.Collections.Generic;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    internal class TranslatableContent : ITranslatableContent
    {
        public string FilePath { get; }
        
        public string XmlContent { get; }

        public IEnumerable<ITranslatablePart> Parts { get; }

        public TranslatableContent(string filePath, string xmlContent, IEnumerable<ITranslatablePart> parts)
        {
            FilePath = filePath;
            XmlContent = xmlContent;
            Parts = parts;
        }
    }
}
