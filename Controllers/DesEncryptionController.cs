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
            _allowedKeySizesInBits = [56, 168];
            _blockSizeInBits = 64;
        }

        public string EncryptDesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.KeySize = keySizeBits;
                des.BlockSize = _blockSizeInBits;
                des.IV = iv;
                des.Key = encryptionKey;

                ICryptoTransform encryptor = des.CreateEncryptor(des.Key, des.IV);

                string encryptedData = EncryptData(encryptor, data);
                return encryptedData;
            }
        }


        public string DecryptDesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.KeySize = keySizeBits;
                des.BlockSize = _blockSizeInBits;
                des.IV = iv;
                des.Key = encryptionKey;

                ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV);

                string decryptedData = DecryptData(decryptor, data);
                return decryptedData;
            }
        }

        public string EncryptTripleDesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.KeySize = keySizeBits;
                tripleDes.BlockSize = _blockSizeInBits;
                tripleDes.IV = iv;
                tripleDes.Key = encryptionKey;

                ICryptoTransform encryptor = tripleDes.CreateEncryptor(tripleDes.Key, tripleDes.IV);

                string encryptedData = EncryptData(encryptor, data);
                return encryptedData;
            }
        }

        public string DecryptTripleDesCsp(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.KeySize = keySizeBits;
                tripleDes.BlockSize = _blockSizeInBits;
                tripleDes.IV = iv;
                tripleDes.Key = encryptionKey;

                ICryptoTransform decryptor = tripleDes.CreateDecryptor(tripleDes.Key, tripleDes.IV);

                string decryptedData = DecryptData(decryptor, data);
                return decryptedData;
            }
        }
    }
}