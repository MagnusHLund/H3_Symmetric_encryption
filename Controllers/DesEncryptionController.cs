using System.Security.Cryptography;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class DesEncryptionController : AbstractEncryptionController, IDesEncryptionController
    {
        private protected override ushort[] _allowedKeySizesInBits { get; }
        private protected override ushort _blockSizeInBits { get; }

        public DesEncryptionController()
        {
            _allowedKeySizesInBits = [64, 192];
            _blockSizeInBits = 64;
        }

        public string EncryptDesCsp(string data, ushort keySizeBits)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                string encryptedData = EncryptData(des, data, keySizeBits);
                return encryptedData;
            }
        }

        public string DecryptDesCsp(string data, ushort keySizeBits)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                string decryptedData = DecryptData(des, data, keySizeBits);
                return decryptedData;
            }
        }

        public string EncryptTripleDesCsp(string data, ushort keySizeBits)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                string encryptedData = EncryptData(tripleDes, data, keySizeBits);
                return encryptedData;
            }
        }

        public string DecryptTripleDesCsp(string data, ushort keySizeBits)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                string decryptedData = DecryptData(tripleDes, data, keySizeBits);
                return decryptedData;
            }
        }
    }
}