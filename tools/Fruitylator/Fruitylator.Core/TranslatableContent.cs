using System.Collections.Generic;

namespace Fruitylator.Core
{
    internal class TranslatableContent : ITranslatableContent
    {
        public string FilePath { get; internal set; }
        
        public string XmlContent { get; internal set; }

        public IEnumerable<ITranslatablePart> Parts { get; internal set; }
    }
}
