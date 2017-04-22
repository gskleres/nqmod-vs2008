using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Fruitylator.Core.Interfaces;

namespace Fruitylator.Core
{
    public class TranslatableFile : ITranslatableFile
    {
        private IDictionary<(string lang, string tag), ITranslatablePart> _translatablesByLang =
            new Dictionary<(string lang, string tag), ITranslatablePart>();


        public ITranslatable Translatable { get; private set; }

        public string OriginalFilePath { get; private set; }

        public static ITranslatableFile OpenRead(string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                var xDoc = XDocument.Load(fileStream);
                if (xDoc.Root == null || !xDoc.Root.HasElements)
                    return new TranslatableFile {OriginalFilePath = filePath};

                const string langPrefix = "Language_";
                var translatableXmlElements = xDoc.Root
                    .Descendants()
                    .Where(d => d.Name.LocalName == langPrefix + "en_US" ||
                                d.Name.LocalName == langPrefix + "DE_DE" ||
                                d.Name.LocalName == langPrefix + "PL_PL" ||
                                d.Name.LocalName == langPrefix + "RU_RU")
                    .ToList();

                if (!translatableXmlElements.Any()) return new TranslatableFile {OriginalFilePath = filePath};

                var translatableParts = new List<ITranslatablePart>();
                foreach (var element in translatableXmlElements)
                {
                    var rows = element.Descendants("Row");
                    foreach (var row in rows)
                    {
                        var language = element.Name.LocalName.Substring(langPrefix.Length);
                        var tag = row.Attribute("Tag");
                        var text = row.Descendants("Text").Single();
                        if (string.IsNullOrWhiteSpace(tag?.Value)) continue;
                        if (string.IsNullOrWhiteSpace(text.Value)) continue;
                        var t = new TranslatablePart(language, tag.Value, filePath)
                        {
                            Text = text.Value
                        };
                        translatableParts.Add(t);
                    }
                }

                return new TranslatableFile
                {
                    Translatable = new Translatable(filePath, xDoc.ToString(), translatableParts),
                    OriginalFilePath = filePath,
                    _translatablesByLang = translatableParts.ToDictionary(p => (p.Language, p.Tag))
                };
            }
        }
    }
}