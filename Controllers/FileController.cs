using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class FileController : IFileController
    {
        private string? _filePath;

        public async Task SaveFileAsync(string data)
        {
            EnsureFilePathIsSet();

            // Use asynchronous file writing
            await File.WriteAllTextAsync(_filePath, data);
        }

        public async Task<string> LoadFileAsync()
        {
            EnsureFilePathIsSet();

            // Use asynchronous file reading
            return await File.ReadAllTextAsync(_filePath);
        }

        public async Task CreateNewFileAsync()
        {
            string fileName = $"{Guid.NewGuid()}.txt";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            _filePath = filePath;

            // Create the file asynchronously
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await stream.FlushAsync(); // Ensure the file is created and flushed
            }
        }

        public async void DeleteFile()
        {
            EnsureFilePathIsSet();

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
                _filePath = null;
            }
        }

        private void EnsureFilePathIsSet()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                throw new InvalidOperationException("No file path has been set.");
            }
        }
    }
}