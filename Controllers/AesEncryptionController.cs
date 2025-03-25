using System.Text;
using System.Security.Cryptography;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class AesEncryptionController : AbstractEncryptionController, IAesEncryptionController
    {
        private protected override ushort[] _allowedKeySizesInBits { get; }
        private protected override ushort _blockSizeInBits { get; }

        // TODO: Look at different paddings, for all algorithms
        public AesEncryptionController()
        {
            _allowedKeySizesInBits = [128, 192, 256];
            _blockSizeInBits = 128;
        }

        public string EncryptAesCsp(string data, ushort keySizeBits)
        {
            using (Aes aes = Aes.Create())
            {
                string encryptedData = EncryptData(aes, data, keySizeBits);
                return encryptedData;
            }
        }

        public string DecryptAesCsp(string data, ushort keySizeBits)
        {
            using (Aes aes = Aes.Create())
            {
                string decryptedData = DecryptData(aes, data, keySizeBits);
                return decryptedData;
            }
        }

        public string EncryptAesManaged(string data, ushort keySizeBits)
        {
            using (AesManaged aesManaged = new AesManaged())
            {
                string encryptedData = EncryptData(aesManaged, data, keySizeBits);
                return encryptedData;
            }
        }

        public string DecryptAesManaged(string data, ushort keySizeBits)
        {
            using (AesManaged aesManaged = new AesManaged())
            {
                string decryptedData = DecryptData(aesManaged, data, keySizeBits);
                return decryptedData;
            }
        }
    }
}