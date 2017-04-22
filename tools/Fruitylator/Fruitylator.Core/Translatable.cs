using System.Collections.Generic;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    internal class Translatable : ITranslatable
    {
        public string OriginalFilePath { get; }

        public string XmlContent { get; }

        public IEnumerable<ITranslatablePart> Parts { get; }

        public Translatable(string originalFilePath, string xmlContent, IEnumerable<ITranslatablePart> parts)
        {
            OriginalFilePath = originalFilePath;
            XmlContent = xmlContent;
            Parts = parts;
        }
    }
}
