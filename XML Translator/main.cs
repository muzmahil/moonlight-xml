using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XML_Translator
{
    public partial class main : Form
    {
        private FileOperations fileOperations = new FileOperations(); // Handles file operations
        private UpdateOperations updateOperations = new UpdateOperations(); // Handles update operations
        private Dictionary<string, string> rightListBoxData = new Dictionary<string, string>(); // Stores modified XML data
        private Dictionary<string, string> sourceListBoxData = new Dictionary<string, string>(); // Stores modified XML data

        public bool autoEncode = true;
        public bool autoSave = false;
        public bool autoSaveFile = false;
        private bool isFileLoaded = false;
        public string saveFilePath = null;
        private const string CurrentVersion = "1.0.3"; // Current version of the application

        public main()
        {
            InitializeComponent();
        }

        // Event handler for the "Open File" button click
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files|*.xml", // Filter to show only XML files
                Title = "Select XML File" // Dialog title
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK) // If the user selects a file
            {
                sourceListBoxData.Clear();
                fileOperations.CurrentFilePath = openFileDialog.FileName; // Store the selected file path
                isFileLoaded = true;
                // Detect the encoding of the XML file
                string encodingName = fileOperations.GetXmlEncoding(fileOperations.CurrentFilePath);
            
                sourceEncoding.SelectedItem = encodingName.ToLower(); // Set the source encoding dropdown
                destEncoding.SelectedItem = encodingName.ToLower(); // Set the destination encoding dropdown

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Register additional encodings


                // Clear UI elements
                sourceList.Items.Clear();
                destList.Items.Clear();
                sourceText.Text = null;
                destText.Text = null;
                sourceItemCountText.Text = "0 / 0";
                destItemCountText.Text = "0 / 0";

                // Load the XML file into the list boxes
                fileOperations.LoadXmlToListBox(fileOperations.CurrentFilePath, encodingName.ToLower(), sourceList, sourceItemCountText,sourceListBoxData);
                sourceList.SelectedIndex = 0; // Select the first item in the source list
            }
        }

        // Event handler for when an item in the source list box is selected
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceList.SelectedItem != null) // If an item is selected
            {
                string selectedId = sourceList.SelectedItem.ToString(); // Get the selected item's id
                if (destList.Items.Contains(selectedId)) // If the item exists in the destination list
                {
                    destList.SelectedItem = selectedId; // Select the item in the destination list
                }
                // Update the source item count text
                sourceItemCountText.Text = (sourceList.SelectedIndex + 1).ToString() + " / " + sourceList.Items.Count.ToString();

                destValueControl(); // Update the destination list controls

                string selectedEncodingName = sourceEncoding.SelectedItem.ToString(); // Get the selected encoding
                Encoding selectedEncoding = Encoding.GetEncoding(selectedEncodingName); // Get the encoding object

                // Display the text for the selected id
                DisplayTextForSelectedId(selectedId, selectedEncoding);
            }
        }

        // Method to display the text for a selected id in the source text box
        private void DisplayTextForSelectedId(string id, Encoding encoding)
        {
            byte[] encodedBytes = encoding.GetBytes(sourceListBoxData[id]);
            string encodedString = encoding.GetString(encodedBytes);
            sourceText.Text = encodedString;
            /* try
             {
                 string xmlContent;
                 using (StreamReader reader = new StreamReader(fileOperations.CurrentFilePath, encoding))
                 {
                     xmlContent = reader.ReadToEnd(); // Read the XML file
                 }

                 XmlDocument xmlDoc = new XmlDocument();
                 xmlDoc.LoadXml(xmlContent); // Load the XML content

                 // Find the <text> node for the selected id
                 XmlNode node = xmlDoc.SelectSingleNode($"//string[@id='{id}']/text");

                 if (node != null) // If the node is found
                 {
                     sourceText.Text = node.InnerText; // Display the text in the source text box
                 }
                 else
                 {
                     sourceText.Text = "No value found."; // Display a message if no text is found
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show error message
             }*/
        }

        // Event handler for when the form is loaded
        private async void main_Load(object sender, EventArgs e)
        {
            await updateOperations.CheckForUpdates(CurrentVersion); // Check for application updates

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Register additional encodings

            // Populate the encoding dropdowns with all available encodings
            foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
            {
                sourceEncoding.Items.Add(encodingInfo.Name);
                destEncoding.Items.Add(encodingInfo.Name);
            }

            // Set the default encoding to UTF-8
            sourceEncoding.SelectedItem = "utf-8";
            sourceEncoding.SelectedIndexChanged += comboBox1_SelectedIndexChanged; // Add event handler for encoding change
            destEncoding.SelectedItem = "utf-8";
            destEncoding.SelectedIndexChanged += comboBox1_SelectedIndexChanged; // Add event handler for encoding change
        }

        // Event handler for when the encoding dropdown selection changes
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sourceList.SelectedItem != null) // If an item is selected in the source list
            {
                string selectedId = sourceList.SelectedItem.ToString(); // Get the selected id

                string selectedEncodingName = sourceEncoding.SelectedItem?.ToString() ?? "utf-8"; // Get the selected encoding
                Encoding selectedEncoding = Encoding.GetEncoding(selectedEncodingName); // Get the encoding object

                // Display the text for the selected id with the new encoding
                DisplayTextForSelectedId(selectedId, selectedEncoding);
            }
        }

        // Event handler for the "Add to Destination" button click
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (sourceList.SelectedItem != null && !destList.Items.Contains(sourceList.SelectedItem)) // If an item is selected and not already in the destination list
            {
                string selectedItem = sourceList.SelectedItem.ToString(); // Get the selected item
                string xmlValue = sourceListBoxData[selectedItem]; // Get the corresponding value from the XML data

                if (!rightListBoxData.ContainsKey(selectedItem)) // If the item is not already in the destination data
                {
                    rightListBoxData[selectedItem] = xmlValue; // Add the item to the destination data
                    destList.Items.Add(selectedItem); // Add the item to the destination list
                    // Update the destination list controls

                    // Move to the next item in the source list
                   

                    // Update the destination item count text
                    destItemCountText.Text = (destList.SelectedIndex + 1).ToString() + " / " + destList.Items.Count.ToString();
                    destValueControl();
                }
            }
            if (!((sourceList.SelectedIndex + 1) + 1 > sourceList.Items.Count))
            {
                sourceList.SelectedIndex++;
            }
        }

        // Method to control the state of destination list controls
        private void destValueControl()
        {
            if (destList.Items.Count > 0) // If there are items in the destination list
            {
                closeFileBtn.Enabled = true;
                closeFileBtn.BackColor = Color.FromArgb(0, 120, 215);
                removeFromDestBtn.Enabled = true; // Enable the remove button
                removeFromDestBtn.BackColor = Color.FromArgb(0, 120, 215); // Set the button color
                destSave.Enabled = true; // Enable the save button
                destSave.BackColor = Color.FromArgb(0, 120, 215); // Set the button color
                saveFileBtn.Enabled = true; // Enable the save file button
                saveFileBtn.BackColor = Color.FromArgb(0, 120, 215); // Set the button color
            }
            else
            {
                closeFileBtn.Enabled = false;
                closeFileBtn.BackColor = Color.DarkGray;
                removeFromDestBtn.Enabled = false; // Disable the remove button
                removeFromDestBtn.BackColor = Color.DarkGray; // Set the button color to dark gray
                destSave.Enabled = false; // Disable the save button
                destSave.BackColor = Color.DarkGray; // Set the button color to dark gray
                saveFileBtn.Enabled = false; // Disable the save file button
                saveFileBtn.BackColor = Color.DarkGray; // Set the button color to dark gray
                destText.Text = null; // Clear the destination text box
            }
        }

        // Event handler for when an item is selected in the destination list
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (destList.SelectedItem != null) // If an item is selected in the destination list
            {
                string selectedId = destList.SelectedItem.ToString(); // Get the selected item's ID
                Encoding selectedEncoding = Encoding.GetEncoding(destEncoding.SelectedItem.ToString()); // Get the selected encoding
                DisplayTextForSelectedIdInRightTextBox(selectedId, selectedEncoding); // Display the text for the selected ID in the right text box
            
                if (autoSaveFileBox.Checked == true && isFileLoaded == true && destList.Items.Count > 0 && saveFilePath != null)
                {
                    fileOperations.SaveXmlFile(saveFilePath, selectedEncoding, rightListBoxData); // Save the XML file
                }
                if (sourceList.Items.Contains(selectedId)) // If the selected ID is in the source list
                {
                    sourceList.SelectedItem = selectedId; // Select the item in the source list
                }
                destItemCountText.Text = (destList.SelectedIndex + 1).ToString() + " / " + destList.Items.Count.ToString(); // Update the item count display

                if (rightListBoxData.ContainsKey(selectedId)) // If the selected ID exists in the data dictionary
                {
                    destText.Text = rightListBoxData[selectedId]; // Set the destination text box content
                }
            }
        }

        // Method to display text for the selected ID in the right text box
        private void DisplayTextForSelectedIdInRightTextBox(string id, Encoding encoding)
        {
            string text = rightListBoxData[id];
            byte[] encodedBytes = encoding.GetBytes(rightListBoxData[id]);
            string encodedString = encoding.GetString(encodedBytes);
            destText.Text = encodedString;
            /*try
            {
                string xmlContent;
                using (StreamReader reader = new StreamReader(fileOperations.CurrentFilePath, encoding)) // Read the XML file with the selected encoding
                {
                    xmlContent = reader.ReadToEnd(); // Read the entire content of the file
                }

                XmlDocument xmlDoc = new XmlDocument(); // Create a new XML document
                xmlDoc.LoadXml(xmlContent); // Load the XML content into the document

                XmlNode node = xmlDoc.SelectSingleNode($"//string[@id='{id}']/text"); // Find the node with the specified ID

                if (node != null) // If the node is found
                {
                    destText.Text = node.InnerText; // Set the destination text box content to the node's text
                }
                else
                {
                    destText.Text = "No value found."; // If the node is not found, display a message
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message if something goes wrong
            }*/
        }

        // Event handler for saving the content of the destination text box
        private void button3_Click(object sender, EventArgs e)
        {

            if (destList.SelectedItem != null) // If an item is selected in the destination list
            {
                string selectedItem = destList.SelectedItem.ToString(); // Get the selected item
                if (rightListBoxData.ContainsKey(selectedItem)) // If the selected item exists in the data dictionary
                {
                    rightListBoxData[selectedItem] = destText.Text; // Update the content in the data dictionary
                    

                }
            }
            else
            {
                MessageBox.Show("Select an item to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning if no item is selected
            }
        }

        // Event handler for saving the XML file
        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFilePath != null)
            {
                string selectedEncodingName = destEncoding.SelectedItem?.ToString() ?? sourceEncoding.SelectedItem.ToString(); // Get the selected encoding
                Encoding selectedEncoding = Encoding.GetEncoding(selectedEncodingName); // Get the selected encoding object

                fileOperations.SaveXmlFile(saveFilePath, selectedEncoding, rightListBoxData); // Save the XML file
                MessageBox.Show("File saved successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "XML Files|*.xml", // Set the filter for XML files
                    Title = "Save XML File" // Set the title of the save file dialog
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK) // If the user selects a file to save
                {
                    saveFilePath = saveFileDialog.FileName; // Get the selected file path

                    string selectedEncodingName = destEncoding.SelectedItem?.ToString() ?? sourceEncoding.SelectedItem.ToString(); // Get the selected encoding
                    Encoding selectedEncoding = Encoding.GetEncoding(selectedEncodingName); // Get the selected encoding object

                    fileOperations.SaveXmlFile(saveFilePath, selectedEncoding, rightListBoxData); // Save the XML file
                    closeFileBtn.Enabled = true;
                    MessageBox.Show("File saved successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a success message
                }
            }
            
        }

        // Event handler for removing an item from the destination list
        private void button5_Click(object sender, EventArgs e)
        {
            if (destList.SelectedItem != null) // If an item is selected in the destination list
            {
                string selectedItem = destList.SelectedItem.ToString(); // Get the selected item

                destList.Items.Remove(selectedItem); // Remove the selected item from the list
                destItemCountText.Text = (destList.SelectedIndex + 1).ToString() + " / " + destList.Items.Count.ToString(); // Update the item count display
                destValueControl(); // Update the controls based on the new list state

                if (rightListBoxData.ContainsKey(selectedItem)) // If the selected item exists in the data dictionary
                {
                    rightListBoxData.Remove(selectedItem); // Remove the selected item from the data dictionary
                }
            }
            else
            {
                MessageBox.Show("Select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Show a warning if no item is selected
            }
        }

        // Event handler for encoding changes in the destination encoding combo box
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (destList.SelectedItem != null) // If an item is selected in the destination list
            {
                int currentCaretPosition = destText.SelectionStart; // Get the current caret position

                Encoding selectedEncoding = Encoding.GetEncoding(destEncoding.SelectedItem.ToString()); // Get the selected encoding
                byte[] encodedBytes = selectedEncoding.GetBytes(rightListBoxData[destList.SelectedItem.ToString()]); // Encode the text with the selected encoding
                string decodedText = selectedEncoding.GetString(encodedBytes); // Decode the text back to the original form

                destText.Text = decodedText; // Set the destination text box content to the decoded text
                destText.SelectionStart = currentCaretPosition; // Restore the caret position
                destText.ScrollToCaret(); // Scroll to the caret position
            }
        }

        // Event handler for text changes in the destination text box
        private void destText_TextChanged(object sender, EventArgs e)
        {
            if (autoEncode == true)
            {

                int currentCaretPosition = destText.SelectionStart; // Get the current caret position

                Encoding selectedEncoding = Encoding.GetEncoding(destEncoding.SelectedItem.ToString()); // Get the selected encoding
                byte[] encodedBytes = selectedEncoding.GetBytes(destText.Text); // Encode the text with the selected encoding
                string decodedText = selectedEncoding.GetString(encodedBytes); // Decode the text back to the original form

                destText.Text = decodedText; // Set the destination text box content to the decoded text
                destText.SelectionStart = currentCaretPosition; // Restore the caret position
                destText.ScrollToCaret(); // Scroll to the caret position
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (autoEncodeBox.Checked == true)
            {
                autoEncode = true;

            }
            else
            {
                autoEncode = false;
            }
        }

        private void autoSaveBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSaveBox.Checked == false)
            {
                autoSave = false;
            }
            else
            {
                autoSave = true;
                destSave.PerformClick();
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (autoSaveFileBox.Checked == true)
            {
                if (saveFilePath == null && isFileLoaded == true)
                {
                    saveFileBtn.PerformClick();
                    autoSaveFile = true;

                }
                else
                {
                    autoSaveFile = true;

                }

            }
            else
            {
                autoSaveFile = false;
            }
        }

        private void main_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void closeFileBtn_Click(object sender, EventArgs e)
        {
            if (saveFilePath == null)
            {

            }
            else
            {
                rightListBoxData.Clear();
                destList.Items.Clear();
                destText.Text = null;
                saveFilePath = null;
            }
        }

        private void openSavedFileBtn_Click(object sender, EventArgs e)
        {
            if (isFileLoaded == true)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "XML Files|*.xml", // Filter to show only XML files
                    Title = "Select XML File" // Dialog title
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK) // If the user selects a file
                {
                    rightListBoxData.Clear();
                    fileOperations.CurrentFilePath = openFileDialog.FileName; // Store the selected file path
                    saveFilePath = openFileDialog.FileName;
                    // Detect the encoding of the XML file
                    string encodingName = fileOperations.GetXmlEncoding(fileOperations.CurrentFilePath);
                    destEncoding.SelectedItem = encodingName.ToLower(); // Set the destination encoding dropdown

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Register additional encodings

                    Encoding selectedEncoding = Encoding.GetEncoding(encodingName.ToLower());

                    destValueControl();
                    // Clear UI elements
                    destList.Items.Clear();
                    destText.Text = null;
                    destItemCountText.Text = "0 / 0";

                    // Load the XML file into the list boxes
                    fileOperations.LoadXmlToListBox(fileOperations.CurrentFilePath, encodingName.ToLower(), destList, destItemCountText,rightListBoxData);
                    destValueControl();
                    sourceList.SelectedIndex = 0; // Select the first item in the source list

                }
            }
            else
            {
                MessageBox.Show("Open XML File First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sourceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveFileBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Right)
            {
                addToDestBtn.PerformClick();
                sourceList.SelectedIndex = sourceList.SelectedIndex - 1;
            }
            else if (e.KeyCode == Keys.Left)
            {
                removeFromDestBtn.PerformClick();
            }
        }

        private void destList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveFileBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.Left)
            {
                removeFromDestBtn.PerformClick();
               

            }
        }

        private void destText_Leave(object sender, EventArgs e)
        {
            if (autoSave == true)
            {
                destSave.PerformClick();

            }
        }
    }
    }