using System.Collections.Generic;

namespace Fruitylator.Core
{
    public interface ITranslatableContent
    {
        string FilePath { get; }

        string XmlContent { get; }

        IEnumerable<ITranslatablePart> Parts { get; }
    }
}