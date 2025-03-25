using H3_Symmetric_encryption.Entities;
using H3_Symmetric_encryption.Interfaces.Controllers;
using H3_Symmetric_encryption.Views;

namespace H3_Symmetric_encryption.Controllers
{
    public class TestResultsController : ITestResultsController
    {
        private readonly IHexadecimalController _hexadecimalController;

        public TestResultsController(IHexadecimalController hexadecimalController)
        {
            _hexadecimalController = hexadecimalController;
        }

        public void HandleViewTestResultsMenu()
        {

        }

        public void SaveTestResults(AlgorithmPerformanceEntity[] algorithmPerformanceEntities)
        {

        }

        public void OutputCipherText(string encryptedData, string decryptedData)
        {
            string encryptedDataHex = _hexadecimalController.ConvertAsciiToHexadecimal(encryptedData);
            string decryptedDataHex = _hexadecimalController.ConvertAsciiToHexadecimal(decryptedData);

            MainView.CustomOutput($"{encryptedData}\n{encryptedDataHex}\n{decryptedData}\n{decryptedDataHex}");
        }
    }
}