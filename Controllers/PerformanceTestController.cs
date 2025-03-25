using System.Diagnostics;
using H3_Symmetric_encryption.Views;
using H3_Symmetric_encryption.Mappers;
using H3_Symmetric_encryption.Entities;
using System.ComponentModel.DataAnnotations;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class PerformanceTestController : IPerformanceTestController
    {
        private readonly IRijndaelEncryptionController _rijndaelEncryptionController;
        private readonly IAesEncryptionController _aesEncryptionController;
        private readonly IDesEncryptionController _desEncryptionController;
        private readonly ITestResultsController _testResultsController;
        private readonly IFileController _fileController;

        public PerformanceTestController(
            IRijndaelEncryptionController rijndaelEncryptionController,
            IAesEncryptionController aesEncryptionController,
            IDesEncryptionController desEncryptionController,
            ITestResultsController testResultsController,
            IFileController fileController
        )
        {
            _rijndaelEncryptionController = rijndaelEncryptionController;
            _aesEncryptionController = aesEncryptionController;
            _desEncryptionController = desEncryptionController;
            _testResultsController = testResultsController;
            _fileController = fileController;
        }

        public async Task HandleTestEncryptionPerformanceMenu()
        {
            int algorithmInput = int.Parse(AlgorithmView.SelectAlgorithmMenu());

            List<AlgorithmEntity> algorithms = AlgorithmMapper.GetAllAlgorithms();
            int algorithmId = algorithms[algorithmInput].AlgorithmId;

            string textToEncrypt = MainView.GetUserInputWithTitle("Enter text to encrypt");

            if (string.IsNullOrEmpty(textToEncrypt))
            {
                throw new ValidationException("Text to encrypt cannot be empty");
            }

            try
            {
                PrepareTests(textToEncrypt);

                var (encryptMethod, decryptMethod, bits) = GetAlgorithmMethods(algorithmInput);
                await PerformTests(encryptMethod, decryptMethod, textToEncrypt, bits, algorithmId);
            }
            finally
            {
                CleanupTests();
            }
        }

        private void PrepareTests(string data)
        {
            _fileController.CreateNewFileAsync();
            _fileController.SaveFileAsync(data);
        }

        private async Task PerformTests(
            Func<string, ushort, string> encryptionMethod,
            Func<string, ushort, string> decryptionMethod,
            string data,
            ushort keySizeBits,
            int algorithmId
        )
        {
            Stopwatch stopwatch = new Stopwatch();

            // In memory encryption
            stopwatch.Start();
            string memoryEncryptedValue = encryptionMethod(data, keySizeBits);
            stopwatch.Stop();
            long memoryEncryptionTime = stopwatch.ElapsedMilliseconds;

            // In memory decryption
            stopwatch.Restart();
            string memoryDecryptedValue = decryptionMethod(memoryEncryptedValue, keySizeBits);
            stopwatch.Stop();
            long memoryDecryptionTime = stopwatch.ElapsedMilliseconds;

            // File encryption
            stopwatch.Restart();
            string fileEncryptedValue = encryptionMethod(await _fileController.LoadFileAsync(), keySizeBits);
            stopwatch.Stop();
            long fileEncryptionTime = stopwatch.ElapsedMilliseconds;

            // File decryption
            stopwatch.Restart();
            string fileDecryptedValue = decryptionMethod(await _fileController.LoadFileAsync(), keySizeBits); // TODO: Research if there is a performance difference in assigning the value to a variable or not. If not, delete it.
            stopwatch.Stop();
            long fileDecryptionTime = stopwatch.ElapsedMilliseconds;

            if (memoryDecryptedValue != data || fileDecryptedValue != data)
            {
                throw new InvalidOperationException("Decrypted values do not match original data");
            }

            AlgorithmPerformanceEntity encryptionResults = CalculateTestResults(memoryEncryptionTime, fileEncryptionTime, keySizeBits, algorithmId, "encryption");
            AlgorithmPerformanceEntity decryptionResults = CalculateTestResults(memoryDecryptionTime, fileDecryptionTime, keySizeBits, algorithmId, "decryption");

            _testResultsController.SaveTestResults([encryptionResults, decryptionResults]);
        }

        private void CleanupTests()
        {
            _fileController.DeleteFile();
        }

        private static AlgorithmPerformanceEntity CalculateTestResults(long memoryCipherTime, long fileCipherTime, int keySizeBits, int algorithmId, string workLoad)
        {
            double secondsPerBlock = memoryCipherTime / 1000.0;
            double bytesPerSecondMemory = keySizeBits / (memoryCipherTime / 1000.0);
            double bytesPerSecondFile = keySizeBits / (fileCipherTime / 1000.0);

            return new AlgorithmPerformanceEntity(
                algorithmId,
                keySizeBits,
                secondsPerBlock,
                bytesPerSecondMemory,
                bytesPerSecondFile,
                workLoad
            );
        }

        private (Func<string, ushort, string> encrypt, Func<string, ushort, string> decrypt, ushort bits) GetAlgorithmMethods(int algorithmInput)
        {
            return algorithmInput switch
            {
                1 => (_aesEncryptionController.EncryptAesCsp, _aesEncryptionController.DecryptAesCsp, 128),
                2 => (_aesEncryptionController.EncryptAesCsp, _aesEncryptionController.DecryptAesCsp, 256),
                3 => (_aesEncryptionController.EncryptAesManaged, _aesEncryptionController.DecryptAesManaged, 128),
                4 => (_aesEncryptionController.EncryptAesManaged, _aesEncryptionController.DecryptAesManaged, 256),
                5 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 128),
                6 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 192),
                7 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 256),
                8 => (_desEncryptionController.EncryptDesCsp, _desEncryptionController.DecryptDesCsp, 56),
                9 => (_desEncryptionController.EncryptTripleDesCsp, _desEncryptionController.DecryptTripleDesCsp, 168),
                _ => throw new InvalidOperationException("Input is out of range")
            };
        }
    }
}