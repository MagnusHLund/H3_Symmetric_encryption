namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IDesEncryptionController
    {
        string EncryptDesCsp(string data, ushort keySizeBits);
        string DecryptDesCsp(string data, ushort keySizeBits);
        string EncryptTripleDesCsp(string data, ushort keySizeBits);
        string DecryptTripleDesCsp(string data, ushort keySizeBits);
    }
}