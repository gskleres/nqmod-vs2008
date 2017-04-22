namespace Fruitylator.Core.Interfaces
{
    public interface ITranslatableFile 
    {
        string OriginalFilePath { get; }

        ITranslatable Translatable { get; }
    }
}