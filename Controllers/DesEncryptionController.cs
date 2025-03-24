using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class DesEncryptionController : AbstractEncryptionController, IDesEncryptionController
    {
        public string DecryptDesCsp(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string DecryptTripleDes(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string EncryptDesCsp(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string EncryptTripleDes(string data, ushort bits)
        {
            throw new NotImplementedException();
        }
    }
}