using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    internal class TranslatablePart : ITranslatablePart
    {
        public string Language { get; }

        public string Tag { get; }

        public string Text { get; set; }

        public string OriginalFilePath { get; }

        public TranslatablePart(string language, string tag, string originalFilePath)
        {
            Language = language;
            Tag = tag;
            OriginalFilePath = originalFilePath;
        }

        public override string ToString() => $"{Tag}, {Language}";
    }
}