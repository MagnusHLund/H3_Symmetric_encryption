using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class FileController : IFileController
    {
        private string? _filePath;

        public async void SaveFileAsync(string data)
        {
            EnsureFileExists(_filePath);

            await File.WriteAllTextAsync(_filePath, data);
        }

        public async Task<string> LoadFileAsync()
        {
            EnsureFileExists(_filePath);

            return await File.ReadAllTextAsync(_filePath);
        }

        public async Task CreateNewFileAsync()
        {
            string fileName = $"{Guid.NewGuid()}.txt";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            _filePath = filePath;

            using (var stream = File.Create(filePath))
            {
                await stream.DisposeAsync();
            }
        }

        public async void DeleteFile()
        {
            EnsureFileExists(_filePath);

            File.Delete(_filePath);
            _filePath = null;
        }

        private void EnsureFileExists(string? filePath)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                throw new InvalidOperationException("No file path has been set.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }
        }
    }
}