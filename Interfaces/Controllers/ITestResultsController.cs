using H3_Symmetric_encryption.Entities;

namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface ITestResultsController
    {
        void HandleViewTestResultsMenu();
        void SaveTestResults(AlgorithmPerformanceEntity[] algorithmPerformanceEntities);
        void CipherOutput OutputCipherText(string encryptedData, string decryptedData);
    }
}