namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IRijndaelEncryptionController
    {
        string EncryptRijndaelManaged(string data, ushort bits);
        string DecryptRijndaelManaged(string data, ushort bits);
    }
}