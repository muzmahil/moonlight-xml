using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string firstLine = reader.ReadLine(); // Read the first line of the file
                    if (firstLine.Contains("encoding")) // Check if the encoding is specified
                    {
                        int encodingStartIndex = firstLine.IndexOf("encoding") + 10; // Find the start of the encoding value
                        int encodingEndIndex = firstLine.IndexOf("\"", encodingStartIndex); // Find the end of the encoding value
                        return firstLine.Substring(encodingStartIndex, encodingEndIndex - encodingStartIndex); // Extract the encoding name
                    }
                }
            }
            catch (Exception)
            {
                // If an error occurs, default to UTF-8
                return "utf-8";
            }

            // If no encoding is found, default to UTF-8
            return "utf-8";
        }

        /// <summary>
        /// Loads XML data from a file into a ListBox and a dictionary.
        /// </summary>
        /// <param name="filePath">The path of the XML file.</param>
        /// <param name="encoding">The encoding to use for reading the file.</param>
        /// <param name="sourceList">The ListBox to populate with XML data.</param>
        /// <param name="sourceItemCountText">The Label to update with the item count.</param>
        public void LoadXmlToListBox(string filePath, Encoding encoding, ListBox sourceList, Label sourceItemCountText, Dictionary<string,string> XmlData)
        {
            try
            {
                string xmlContent;
                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    xmlContent = reader.ReadToEnd(); // Read the entire XML file
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent); // Load the XML content into an XmlDocument

                XmlNodeList stringNodes = xmlDoc.SelectNodes("//string"); // Select all <string> nodes

                sourceList.Items.Clear(); // Clear the source ListBox
                XmlData.Clear(); // Clear the XML data dictionary

                // Iterate through each <string> node
                foreach (XmlNode node in stringNodes)
                {
                    string id = node.Attributes["id"]?.Value; // Get the "id" attribute
                    string text = node["text"]?.InnerText ?? string.Empty; // Get the "text" element's inner text

                    if (!string.IsNullOrEmpty(id)) // If the id is not empty
                    {
                        XmlData[id] = text; // Add the id and text to the dictionary
                        sourceList.Items.Add(id); // Add the id to the source ListBox
                    }
                }

                // Update the source item count text
                sourceItemCountText.Text = $"0 / {sourceList.Items.Count}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}"); // Throw an exception if an error occurs
            }
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
                XmlDocument xmlDoc = new XmlDocument();

                // Create the XML declaration
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", encoding.WebName, null);
                xmlDoc.AppendChild(xmlDeclaration);

                // Create the root element
                XmlElement stringTableElement = xmlDoc.CreateElement("string_table");
                xmlDoc.AppendChild(stringTableElement);

                // Iterate through the modified data and create XML elements
                foreach (var item in rightListBoxData)
                {
                    XmlElement stringElement = xmlDoc.CreateElement("string");
                    stringElement.SetAttribute("id", item.Key); // Set the "id" attribute

                    XmlElement textElement = xmlDoc.CreateElement("text");
                    textElement.InnerText = item.Value; // Set the "text" element's inner text

                    stringElement.AppendChild(textElement); // Append the text element to the string element
                    stringTableElement.AppendChild(stringElement); // Append the string element to the root element
                }

                xmlDoc.Save(filePath); // Save the XML document to the specified file
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}"); // Throw an exception if an error occurs
            }
        }
    }
}