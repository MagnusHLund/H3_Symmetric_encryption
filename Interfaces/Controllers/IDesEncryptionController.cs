namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IDesEncryptionController
    {
        string EncryptDesCsp(string data, ushort bits);
        string DecryptDesCsp(string data, ushort bits);
        string EncryptTripleDes(string data, ushort bits);
        string DecryptTripleDes(string data, ushort bits);
    }
}