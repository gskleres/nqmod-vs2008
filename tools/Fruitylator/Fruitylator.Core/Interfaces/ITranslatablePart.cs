namespace Fruitylator.Core.Interfaces
{
    public interface ITranslatablePart
    {
        string Language { get; }

        string Tag { get; }

        string Text { get; set; }
    }
}