namespace Fruitylator.Events
{
    public class FileChangedEvent
    {
        public string FileName { get; private set; }

        public FileChangedEvent(string fileName)
        {
            FileName = fileName;
        }
    }
}
