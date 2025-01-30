using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace XML_Translator
{
    /// <summary>
    /// Handles file-related operations such as loading and saving XML files.
    /// </summary>
    public class FileOperations
    {
        // Stores the path of the currently loaded XML file
        public string CurrentFilePath { get; set; }

        // Stores the XML data (id and text pairs) loaded from the file

        /// <summary>
        /// Detects the encoding of the XML file by reading the first line.
        /// </summary>
        /// <param name="filePath">The path of the XML file.</param>
        /// <returns>The encoding name (e.g., "utf-8").</returns>
        public string GetXmlEncoding(string filePath)
        {
            string encoding = null;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string firstLine = reader.ReadLine(); // İlk satırı oku

                if (!string.IsNullOrEmpty(firstLine) && firstLine.ToLower().Contains("encoding"))
                {
                    // Encoding özniteliğini yakalamak için regex
                    Match match = Regex.Match(firstLine, @"encoding\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        encoding = match.Groups[1].Value; // Encoding değerini al
                    }
                }
            }

            return encoding;
        }

        /// <summary>
        /// Loads XML data from a file into a ListBox and a dictionary.
        /// </summary>
        /// <param name="filePath">The path of the XML file.</param>
        /// <param name="encoding">The encoding to use for reading the file.</param>
        /// <param name="sourceList">The ListBox to populate with XML data.</param>
        /// <param name="sourceItemCountText">The Label to update with the item count.</param>
        public void LoadXmlToListBox(string filePath, string encoding, ListBox sourceList, Label sourceItemCountText, Dictionary<string, string> XmlData)
        {
            try
            {
                string xmlContent;
                Encoding enc = Encoding.GetEncoding(encoding); // Belirtilen encoding ile aç

                using (StreamReader reader = new StreamReader(filePath, enc))
                {
                    xmlContent = reader.ReadToEnd(); // XML dosyasını oku
                }

                // Hatalı karakterleri temizle
                xmlContent = FixXmlEntities(xmlContent);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent); // XML içeriğini yükle
                XmlNodeList stringNodes = xmlDoc.SelectNodes("//string"); // <string> düğümlerini seç

                sourceList.Items.Clear();
                XmlData.Clear();

                foreach (XmlNode node in stringNodes)
                {
                    string id = node.Attributes["id"]?.Value; // "id" al
                    string text = node["text"]?.InnerText ?? string.Empty; // "text" içeriğini al

                    if (!string.IsNullOrEmpty(id))
                    {
                        XmlData[id] = text; // Dictionary'ye ekle
                        sourceList.Items.Add(id); // ListBox'a ekle
                    }
                }

                sourceItemCountText.Text = $"0 / {sourceList.Items.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"XML Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // XML içindeki hatalı karakterleri düzelten fonksiyon
        private string FixXmlEntities(string xml)
        {
            return Regex.Replace(xml, @"(?<=<text>)(.*?)(?=</text>)", match =>
            {
                return match.Value
                    .Replace("&", "&amp;")
                    .Replace("'", "&apos;")
                    .Replace("\"", "&quot;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\n", "&#10;")
                    .Replace("\r", "");
            }, RegexOptions.Singleline);
        }
        /// <summary>
        /// Saves modified XML data to a file.
        /// </summary>
        /// <param name="filePath">The path where the XML file will be saved.</param>
        /// <param name="encoding">The encoding to use for writing the file.</param>
        /// <param name="rightListBoxData">The dictionary containing modified XML data.</param>
        public void SaveXmlFile(string filePath, Encoding encoding, Dictionary<string, string> rightListBoxData)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true, // Gerekirse girinti
                    NewLineOnAttributes = true, // Satır başında özellikleri düzenle
                    Encoding = encoding, // Dosya kodlaması
                    ConformanceLevel = ConformanceLevel.Document // Doküman seviyesi uyum
                };

                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("string_table");

                    foreach (var item in rightListBoxData)
                    {
                        writer.WriteStartElement("string");
                        writer.WriteAttributeString("id", item.Key);

                        // 'InnerText' yerine doğrudan 'text' elementini yazıyoruz
                        writer.WriteStartElement("text");

                        // Metni düz yaz (escape etmiyoruz)
                        writer.WriteRaw(FixSaveXmlEntities(item.Value));

                        writer.WriteEndElement(); // </text>
                        writer.WriteEndElement(); // </string>
                    }

                    writer.WriteEndElement(); // </string_table>
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        private string FixSaveXmlEntities(string xml)
        {
            return Regex.Replace(xml, @"(?<=<text>)(.*?)(?=</text>)", match =>
            {
                return match.Value
                .Replace("&amp;", "&")  // &amp; ifadesini & ile değiştir
        .Replace("'", "&apos;")  // ' karakterini &apos; ile değiştir
        .Replace("\"", "&quot;") // " karakterini &quot; ile değiştir
        .Replace("<", "&lt;")    // < karakterini &lt; ile değiştir
        .Replace(">", "&gt;")    // > karakterini &gt; ile değiştir
        .Replace("\n", "&#10;")  // Satır sonu karakterini &#10; ile değiştir
        .Replace("\r", "&#13;"); // Carriage return karakterini &#13; ile değiştir
            }, RegexOptions.Singleline);
        }



    }
}