namespace Fruitylator.Core
{
    public interface ITranslatablePart
    {
        string Language { get; }
        string Tag { get; }
        string Text { get; set; }
    }
}