using System.Collections.Generic;

namespace Fruitylator.Core.Interfaces
{
    public interface ITranslatable
    {
        string OriginalFilePath { get; }

        string XmlContent { get; }

        IEnumerable<ITranslatablePart> Parts { get; }
    }
}