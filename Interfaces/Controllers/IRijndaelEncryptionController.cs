namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IRijndaelEncryptionController
    {
        string EncryptRijndaelManaged(string data, ushort keySizeBits);
        string DecryptRijndaelManaged(string data, ushort keySizeBits);
    }
}