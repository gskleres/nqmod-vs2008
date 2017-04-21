using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    internal class TranslatablePart : ITranslatablePart
    {
        public string Language { get; set; }

        public string Tag { get; set;  }

        public string Text { get; set; }
    }
}