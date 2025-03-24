namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IAesEncryptionController
    {
        string EncryptAesCsp(string data, ushort bits);
        string DecryptAesCsp(string data, ushort bits);
        string EncryptAesManaged(string data, ushort bits);
        string DecryptAesManaged(string data, ushort bits);
    }
}