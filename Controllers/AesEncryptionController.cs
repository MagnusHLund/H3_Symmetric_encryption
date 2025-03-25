using System.Text;
using System.Security.Cryptography;
using H3_Symmetric_encryption.Interfaces.Controllers;

namespace H3_Symmetric_encryption.Controllers
{
    public class AesEncryptionController : AbstractEncryptionController, IAesEncryptionController
    {
        private protected override ushort[] _allowedKeySizesInBits { get; }
        private protected override ushort _blockSizeInBits { get; }

        public AesEncryptionController()
        {
            _allowedKeySizesInBits = [128, 192, 256];
            _blockSizeInBits = 128;
        }

        public string EncryptAesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySizeBits;
                aes.BlockSize = _blockSizeInBits;
                aes.IV = iv;
                aes.Key = encryptionKey;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                string encryptedData = EncryptData(encryptor, data);
                return encryptedData;
            }
        }

        public string DecryptAesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySizeBits;
                aes.BlockSize = _blockSizeInBits;
                aes.IV = iv;
                aes.Key = encryptionKey;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                string decryptedData = DecryptData(decryptor, data);
                return decryptedData;
            }
        }

        public string EncryptAesManaged(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.KeySize = keySizeBits;
                aesManaged.BlockSize = _blockSizeInBits;
                aesManaged.IV = iv;
                aesManaged.Key = encryptionKey;

                ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);

                string encryptedData = EncryptData(encryptor, data);
                return encryptedData;
            }
        }

        public string DecryptAesManaged(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.KeySize = keySizeBits;
                aesManaged.BlockSize = _blockSizeInBits;
                aesManaged.IV = iv;
                aesManaged.Key = encryptionKey;

                ICryptoTransform decryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);

                string decryptedData = DecryptData(decryptor, data);
                return decryptedData;
            }
        }
    }
}