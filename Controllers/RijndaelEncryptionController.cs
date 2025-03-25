using System.Text;
using System.Security.Cryptography;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class RijndaelEncryptionController : AbstractEncryptionController, IRijndaelEncryptionController
    {
        private protected override ushort[] _allowedKeySizesInBits { get; }
        private protected override ushort _blockSizeInBits { get; }

        public RijndaelEncryptionController()
        {
            _allowedKeySizesInBits = [128, 192, 256];
            _blockSizeInBits = 128;
        }

        public string EncryptRijndaelManaged(string data, ushort keySizeBits)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                string encryptedData = EncryptData(rijndael, data, keySizeBits);
                return encryptedData;
            }
        }

        public string DecryptRijndaelManaged(string data, ushort keySizeBits)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                string encryptedData = DecryptData(rijndael, data, keySizeBits);
                return encryptedData;
            }
        }
    }
}