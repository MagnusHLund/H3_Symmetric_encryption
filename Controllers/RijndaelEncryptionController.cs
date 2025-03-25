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
            _blockSizeInBits = 256;
        }

        public string EncryptRijndaelManaged(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = keySizeBits;
                rijndael.BlockSize = _blockSizeInBits;
                rijndael.IV = iv;
                rijndael.Key = encryptionKey;

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedData = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

                string encryptedDataString = Convert.ToBase64String(encryptedData);
                return encryptedDataString;
            }
        }

        public string DecryptRijndaelManaged(string data, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] iv = GenerateIv();

            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.KeySize = keySizeBits;
                rijndael.BlockSize = _blockSizeInBits;
                rijndael.IV = iv;
                rijndael.Key = encryptionKey;

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                byte[] encryptedData = Convert.FromBase64String(data);
                byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                string decryptedDataString = Encoding.UTF8.GetString(decryptedData);
                return decryptedDataString;
            }
        }
    }
}