using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XML_Translator
{
    /// <summary>
    /// Handles checking for updates and updating the application.
    /// </summary>
    public class UpdateOperations
    {
        // GitHub API URL to fetch the latest release information
        private const string ApiUrl = "https://api.github.com/repos/muzmahil/moonlight-xml/releases/latest";

        /// <summary>
        /// Checks for updates by comparing the current version with the latest version on GitHub.
        /// </summary>
        /// <param name="currentVersion">The current version of the application.</param>
        public async Task CheckForUpdates(string currentVersion)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "WindowsFormsApp"); // Set the user agent
                    string response = await client.GetStringAsync(ApiUrl); // Get the latest release information from GitHub

                    // Extract the latest version number from the response
                    string latestVersion = GetBetween(response, "\"tag_name\":\"", "\"");

                    // Compare the current version with the latest version
                    if (latestVersion != currentVersion && Convert.ToDouble(currentVersion) < Convert.ToDouble(latestVersion))
                    {
                        string downloadUrl = GetDownloadUrl(response); // Get the download URL for the new version

                        // Prompt the user to update
                        DialogResult result = MessageBox.Show(
                            $"A new update is available: {latestVersion}. Would you like to update now?",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes) // If the user chooses to update
                        {
                            await UpdateApplication(downloadUrl); // Update the application
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during the update check: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show error message
            }
        }

        /// <summary>
        /// Downloads and installs the latest version of the application.
        /// </summary>
        /// <param name="downloadUrl">The URL to download the new version.</param>
        private async Task UpdateApplication(string downloadUrl)
        {
            try
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "MainApp_New.exe"); // Temporary file path for the new version

                using (HttpClient webClient = new HttpClient())
                {
                    // Download the new version
                    byte[] fileBytes = await webClient.GetByteArrayAsync(downloadUrl);
                    File.WriteAllBytes(tempFilePath, fileBytes); // Save the new version to the temporary file
                }

                string currentFilePath = Application.ExecutablePath; // Get the current application path
                string backupFilePath = currentFilePath + ".bak"; // Backup file path

                // Backup the current application
                if (File.Exists(backupFilePath))
                    File.Delete(backupFilePath);

                File.Move(currentFilePath, backupFilePath); // Move the current application to the backup file

                // Replace the current application with the new version
                File.Move(tempFilePath, currentFilePath);

                MessageBox.Show("The application has been successfully updated. Restarting.", "Update Completed", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message

                // Restart the application
                Process.Start(currentFilePath);
                Application.Exit(); // Exit the current instance
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during the update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show error message
            }
        }

        /// <summary>
        /// Extracts a substring between two specified strings.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="start">The start string.</param>
        /// <param name="end">The end string.</param>
        /// <returns>The substring between the start and end strings.</returns>
        private string GetBetween(string source, string start, string end)
        {
            int startIndex = source.IndexOf(start) + start.Length; // Find the start index
            int endIndex = source.IndexOf(end, startIndex); // Find the end index
            return source.Substring(startIndex, endIndex - startIndex); // Extract the substring
        }

        /// <summary>
        /// Extracts the download URL from the GitHub release JSON.
        /// </summary>
        /// <param name="releaseJson">The JSON response from the GitHub API.</param>
        /// <returns>The download URL for the latest release.</returns>
        private string GetDownloadUrl(string releaseJson)
        {
            string assetUrl = GetBetween(releaseJson, "\"browser_download_url\":\"", "\""); // Extract the download URL
            return assetUrl;
        }
    }
}