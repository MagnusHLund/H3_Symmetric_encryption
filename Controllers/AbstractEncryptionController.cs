using System.Text;
using System.Security.Cryptography;

namespace H3_Symmetric_encryption.Controllers
{
    public abstract class AbstractEncryptionController
    {
        private protected abstract ushort[] _allowedKeySizesInBits { get; }
        private protected abstract ushort _blockSizeInBits { get; }

        private protected byte[] GenerateIv()
        {
            int ivSize = _blockSizeInBits / 8;

            byte[] iv = new byte[ivSize];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(iv);
            }

            return iv;
        }

        private protected byte[] GetEncryptionKey(ushort keySizeBits)
        {
            // The keys could have been stored in a more secure way, but this is good enough as an example.
            // We used a git ignored appsettings.json file, for production, in the H3_ReelTok project.
            // It would also be possible to use a key vault, to store the keys, but that is overkill for this project.

            if (!_allowedKeySizesInBits.Contains(keySizeBits))
            {
                throw new ArgumentException("Invalid key size.");
            }

            string encryptionKey = keySizeBits switch
            {
                64 => "j!Yd2f3g",
                128 => "d/2favmNncA24sku",
                192 => "jSA3fdxCvuf4!9FklAsfv72H",
                256 => "FDvc1#8fjmdsa843lscvms12vdAl3!vI",
                _ => throw new ArgumentException("Invalid key size.")
            };

            byte[] encodedEncryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            return encodedEncryptionKey;
        }

        private protected string EncryptData(SymmetricAlgorithm algorithm, string dataToEncrypt, ushort KeySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(KeySizeBits);
            byte[] iv = GenerateIv();

            algorithm.KeySize = KeySizeBits;
            algorithm.BlockSize = _blockSizeInBits;
            algorithm.IV = iv;
            algorithm.Key = encryptionKey;

            ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

            byte[] dataBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
            byte[] encryptedData = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

            byte[] combinedData = new byte[iv.Length + encryptedData.Length];
            Buffer.BlockCopy(iv, 0, combinedData, 0, iv.Length);
            Buffer.BlockCopy(encryptedData, 0, combinedData, iv.Length, encryptedData.Length);

            return Convert.ToBase64String(combinedData);
        }

        private protected string DecryptData(SymmetricAlgorithm algorithm, string dataToDecrypt, ushort keySizeBits)
        {
            byte[] encryptionKey = GetEncryptionKey(keySizeBits);
            byte[] encryptedDataWithIv = Convert.FromBase64String(dataToDecrypt);

            algorithm.KeySize = keySizeBits;
            algorithm.BlockSize = _blockSizeInBits;
            algorithm.Key = encryptionKey;

            var (iv, encryptedData) = SeparateIvAndEncryptedData(encryptedDataWithIv, algorithm);

            algorithm.IV = iv;

            ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);
            byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(decryptedData);
        }

        private static (byte[] iv, byte[] encryptedData) SeparateIvAndEncryptedData(byte[] encryptedDataWithIv, SymmetricAlgorithm algorithm)
        {
            byte[] iv = new byte[algorithm.BlockSize / 8];
            byte[] encryptedData = new byte[encryptedDataWithIv.Length - iv.Length];
            Buffer.BlockCopy(encryptedDataWithIv, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(encryptedDataWithIv, iv.Length, encryptedData, 0, encryptedData.Length);

            return (iv, encryptedData);
        }
    }
}
