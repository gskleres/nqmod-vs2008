using System.Collections.Generic;

namespace Fruitylator.Core.Interfaces
{
    public interface ITranslatableContent
    {
        string FilePath { get; }

        string XmlContent { get; }

        IEnumerable<ITranslatablePart> Parts { get; }
    }
}