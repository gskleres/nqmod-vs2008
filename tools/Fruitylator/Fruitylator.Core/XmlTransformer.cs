using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Fruitylator.Core
{
    [Export(typeof(IXmlTransformer))]
    internal class XmlTransformer : IXmlTransformer
    {
        public ITranslatableContent LoadFile(string path)
        {
            if (!File.Exists(path)) return null;
            using (var fileStream = File.OpenRead(path))
            {
                var xDoc = XDocument.Load(fileStream);

                if (xDoc.Root == null || !xDoc.Root.HasElements) return null;

                const string langPrefix = "Language_";
                var translatableXmlElements = xDoc.Root.Descendants().Where(d => d.Name.LocalName == langPrefix + "en_US" ||
                                                                                 d.Name.LocalName == langPrefix + "DE_DE" ||
                                                                                 d.Name.LocalName == langPrefix + "PL_PL" ||
                                                                                 d.Name.LocalName == langPrefix + "RU_RU")
                    .ToList();

                if (!translatableXmlElements.Any()) return null;

                var translatableContent = new TranslatableContent
                {
                    FilePath = path,
                    XmlContent = xDoc.ToString()
                };

                var translatableParts = new List<ITranslatablePart>();
                foreach (var element in translatableXmlElements)
                {
                    var rows = element.Descendants("Row");
                    foreach (var row in rows)
                    {
                        var tag = row.Attribute("Tag");

                        if (string.IsNullOrWhiteSpace(tag?.Value)) continue;

                        var text = row.Descendants("Text").Single();

                        translatableParts.Add(new TranslatablePart
                        {
                            Language = element.Name.LocalName.Substring(langPrefix.Length),
                            Tag = tag.Value,
                            Text = text.Value
                        });
                    }
                }

                translatableContent.Parts = translatableParts;
                return translatableContent;
            }
        }
    }

    public interface IXmlTransformer
    {
        ITranslatableContent LoadFile(string path);
    }
}