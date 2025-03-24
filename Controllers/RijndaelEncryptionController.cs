using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class RijndaelEncryptionController : AbstractEncryptionController, IRijndaelEncryptionController
    {
        public string DecryptRijndaelManaged(string data, ushort bits)
        {
            throw new NotImplementedException();
        }

        public string EncryptRijndaelManaged(string data, ushort bits)
        {
            throw new NotImplementedException();
        }
    }
}