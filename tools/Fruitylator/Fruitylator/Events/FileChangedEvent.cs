using Fruitylator.Core.Interfaces;

namespace Fruitylator.Events
{
    public class FileChangedEvent
    {
        public ITranslatableFile Translatable { get; private set; }

        public FileChangedEvent(ITranslatableFile translatable)
        {
            Translatable = translatable;
        }
    }
}
