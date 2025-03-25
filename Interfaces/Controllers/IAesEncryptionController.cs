namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IAesEncryptionController
    {
        string EncryptAesCsp(string data, ushort keySizeBits);
        string DecryptAesCsp(string data, ushort keySizeBits);
        string EncryptAesManaged(string data, ushort keySizeBits);
        string DecryptAesManaged(string data, ushort keySizeBits);
    }
}