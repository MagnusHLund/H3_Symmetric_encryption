using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class AesEncryptionController : AbstractEncryptionController, IAesEncryptionController
    {
        public string DecryptAesCsp(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string DecryptAesManaged(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string EncryptAesCsp(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string EncryptAesManaged(string data, ushort bits)
        {
            throw new NotImplementedException();
        }
    }
}