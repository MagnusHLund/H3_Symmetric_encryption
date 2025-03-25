using H3_Symmetric_encryption.Views;
using H3_Symmetric_encryption.Entities;
using H3_Symmetric_encryption.Interfaces.Controllers;
using H3_Symmetric_encryption.Interfaces.Repositories;
using H3_Symmetric_encryption.Models;

namespace H3_Symmetric_encryption.Controllers
{
    public class TestResultsController : ITestResultsController
    {
        private readonly IAlgorithmPerformanceRepository _algorithmPerformanceRepository;
        private readonly IHexadecimalController _hexadecimalController;

        public TestResultsController(IAlgorithmPerformanceRepository algorithmPerformanceRepository, IHexadecimalController hexadecimalController)
        {
            _algorithmPerformanceRepository = algorithmPerformanceRepository;
            _hexadecimalController = hexadecimalController;
        }

        public void HandleViewTestResultsMenu()
        {
            byte workloadUserInput = byte.Parse(TestResultsView.ShowEncryptionOrDecryptionMenu());

            PerformanceTable[] testResultTables = workloadUserInput switch
            {
                1 => GetTestResultTables("encryption"),
                2 => GetTestResultTables("decryption"),
                _ => throw new ArgumentException("Invalid input. Please select '1' for Encryption or '2' for Decryption.")
            };

            string[] tableMenuOptions = testResultTables
                .Select(table => table.TableName)
                .ToArray();

            ushort tableUserInput = ushort.Parse(MainView.CustomMenu(tableMenuOptions));

            PerformanceTable selectedTable = testResultTables[tableUserInput - 1];

            selectedTable.DrawTable();

            MainView.GetUserInputWithTitle("Press enter to return to the main menu");
        }

        public void SaveTestResults(AlgorithmPerformanceEntity[] algorithmPerformanceEntities)
        {
            _algorithmPerformanceRepository.SavePerformanceTestResults(algorithmPerformanceEntities);
        }

        public void OutputCipherText(string encryptedData, string decryptedData)
        {
            string encryptedDataHex = _hexadecimalController.ConvertAsciiToHexadecimal(encryptedData);
            string decryptedDataHex = _hexadecimalController.ConvertAsciiToHexadecimal(decryptedData);

            MainView.CustomOutput(
                $"Encrypted ascii data: {encryptedData}\n" +
                $"Encrypted hex data: {encryptedDataHex}\n" +
                $"Decrypted ascii data: {decryptedData}\n" +
                $"Decrypted hex data: {decryptedDataHex}"
            );
        }

        private PerformanceTable[] GetTestResultTables(string workload)
        {
            List<AlgorithmPerformanceEntity> testResults = _algorithmPerformanceRepository.GetPerformanceTestResults(workload);

            if (testResults.Count == 0)
            {
                throw new InvalidDataException("No test results found for the selected workload");
            }

            PerformanceTable[] performanceTables = testResults
                .GroupBy(testResult => testResult.PlainTextSizeInBytes)
                .Select(group => new PerformanceTable(
                    group.ToList(),
                    group.Key,
                    workload
                ))
                .ToArray();

            return performanceTables;
        }
    }
}