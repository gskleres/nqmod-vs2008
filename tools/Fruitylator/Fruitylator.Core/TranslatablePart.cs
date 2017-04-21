namespace Fruitylator.Core
{
    internal class TranslatablePart : ITranslatablePart
    {
        public string Language { get; internal set; }

        public string Tag { get; internal set; }

        public string Text { get; set; }
    }
}