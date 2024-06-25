using Azure.Storage.Blobs;
using System.Text.RegularExpressions;

namespace RSAllies.AzureStorage
{
    public static class StorageService
    {
        private static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=rsallies;AccountKey=JrtUOjQUrMW1TZQ6zMxlhecmBU+7/EdYZHbZuAvAzUoKZaDlgVf/yqhx05mMArXpyRawXvMUvARe+ASt+pjPew==;EndpointSuffix=core.windows.net";

        private static string ContainerName = "rsalliesblobs";

        public static async Task<string> UploadFileAsync(Guid fileName, Stream fileStream)
        {
            var blobContainerClient = new BlobContainerClient(ConnectionString, ContainerName);
            blobContainerClient.CreateIfNotExists();

            var sanitizedFileName = SanitizeFileName(fileName.ToString());
            var blobClient = blobContainerClient.GetBlobClient(sanitizedFileName);
            await blobClient.UploadAsync(fileStream, true);
            return blobClient.Uri.ToString();
        }

        private static string SanitizeFileName(string fileName)
        {
            // Convert to lower case
            fileName = fileName.ToLowerInvariant();

            // Replace invalid characters with hyphens
            fileName = Regex.Replace(fileName, @"[^a-z0-9\-]", "-");

            // Ensure the name starts and ends with a letter or number
            fileName = Regex.Replace(fileName, @"^-+", "");
            fileName = Regex.Replace(fileName, @"-+$", "");

            // Ensure the name is within valid length limits
            if (fileName.Length < 3) fileName = fileName.PadRight(3, 'a');
            if (fileName.Length > 63) fileName = fileName.Substring(0, 63);

            return fileName;
        }
    }
}
