using System.Diagnostics;
using H3_Symmetric_encryption.Views;
using H3_Symmetric_encryption.Mappers;
using H3_Symmetric_encryption.Entities;
using System.ComponentModel.DataAnnotations;
using H3_Symmetric_encryption.Interfaces.Controllers;
using H3_Symmetric_encryption.Utils;

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
            List<AlgorithmEntity> algorithms = AlgorithmMapper.GetAllAlgorithms();
            string[] algorithmNames = algorithms.Select(a => a.Name).ToArray();

            int algorithmInput = int.Parse(MainView.CustomMenu(algorithmNames));
            int algorithmId = algorithms[algorithmInput - 1].AlgorithmId;

            string textToEncrypt = MainView.GetUserInputWithTitle("Enter text to encrypt");

            if (string.IsNullOrEmpty(textToEncrypt))
            {
                throw new ValidationException("Text to encrypt cannot be empty");
            }

            try
            {
                await PrepareTests(textToEncrypt);

                var (encryptMethod, decryptMethod, bits) = GetAlgorithmMethods(algorithmInput);
                await PerformTestsAsync(encryptMethod, decryptMethod, textToEncrypt, bits, algorithmId);
            }
            finally
            {
                CleanupTests();
            }
        }

        private async Task PrepareTests(string data)
        {
            await _fileController.CreateNewFileAsync();
            await _fileController.SaveFileAsync(data);
        }

        private async Task PerformTestsAsync(
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
            long memoryEncryptionTicks = stopwatch.ElapsedTicks;

            // In memory decryption
            stopwatch.Restart();
            string memoryDecryptedValue = decryptionMethod(memoryEncryptedValue, keySizeBits);
            stopwatch.Stop();
            long memoryDecryptionTicks = stopwatch.ElapsedTicks;

            // File encryption
            stopwatch.Restart();
            string fileEncryptedValue = encryptionMethod(await _fileController.LoadFileAsync(), keySizeBits);
            stopwatch.Stop();
            long fileEncryptionTicks = stopwatch.ElapsedTicks;

            // Update file to include encrypted data
            await _fileController.SaveFileAsync(fileEncryptedValue);

            // File decryption
            stopwatch.Restart();
            string fileDecryptedValue = decryptionMethod(await _fileController.LoadFileAsync(), keySizeBits);
            stopwatch.Stop();
            long fileDecryptionTicks = stopwatch.ElapsedTicks;

            if (memoryDecryptedValue != data || fileDecryptedValue != data)
            {
                throw new InvalidOperationException("Decrypted values do not match original data");
            }

            int DataSizeInBytes = data.Length;

            double memoryEncryptionTime = StopWatchUtils.ConvertTicksToMilliSeconds(memoryEncryptionTicks);
            double memoryDecryptionTime = StopWatchUtils.ConvertTicksToMilliSeconds(memoryDecryptionTicks);
            double fileEncryptionTime = StopWatchUtils.ConvertTicksToMilliSeconds(fileEncryptionTicks);
            double fileDecryptionTime = StopWatchUtils.ConvertTicksToMilliSeconds(fileDecryptionTicks);

            AlgorithmPerformanceEntity encryptionResults = CalculateTestResults(memoryEncryptionTime, fileEncryptionTime, keySizeBits, algorithmId, "encryption", DataSizeInBytes);
            AlgorithmPerformanceEntity decryptionResults = CalculateTestResults(memoryDecryptionTime, fileDecryptionTime, keySizeBits, algorithmId, "decryption", DataSizeInBytes);

            _testResultsController.SaveTestResults([encryptionResults, decryptionResults]);
            _testResultsController.OutputCipherText(memoryEncryptedValue, fileDecryptedValue);
        }

        private void CleanupTests()
        {
            _fileController.DeleteFile();
        }

        private static AlgorithmPerformanceEntity CalculateTestResults(double memoryCipherTime, double fileCipherTime, int keySizeBits, int algorithmId, string workLoad, int DataSizeInBytes)
        {
            double secondsPerBlock = memoryCipherTime / 1000.0;
            double bytesPerSecondMemory = keySizeBits / (memoryCipherTime / 1000.0);
            double bytesPerSecondFile = keySizeBits / (fileCipherTime / 1000.0);

            return new AlgorithmPerformanceEntity(
                algorithmId,
                DataSizeInBytes,
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
                2 => (_aesEncryptionController.EncryptAesCsp, _aesEncryptionController.DecryptAesCsp, 192),
                3 => (_aesEncryptionController.EncryptAesCsp, _aesEncryptionController.DecryptAesCsp, 256),
                4 => (_aesEncryptionController.EncryptAesManaged, _aesEncryptionController.DecryptAesManaged, 128),
                5 => (_aesEncryptionController.EncryptAesManaged, _aesEncryptionController.DecryptAesManaged, 192),
                6 => (_aesEncryptionController.EncryptAesManaged, _aesEncryptionController.DecryptAesManaged, 256),
                7 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 128),
                8 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 192),
                9 => (_rijndaelEncryptionController.EncryptRijndaelManaged, _rijndaelEncryptionController.DecryptRijndaelManaged, 256),
                10 => (_desEncryptionController.EncryptDesCsp, _desEncryptionController.DecryptDesCsp, 64),
                11 => (_desEncryptionController.EncryptTripleDesCsp, _desEncryptionController.DecryptTripleDesCsp, 192),
                _ => throw new InvalidOperationException("Input is out of range")
            };
        }
    }
}