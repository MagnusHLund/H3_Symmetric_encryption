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
                56 => "j!Yd2f3",
                128 => "d/2favmNncA24sku",
                168 => "23fjvc8x9sa1&ffvxamdu",
                192 => "jSA3fdxCvuf4!9FklAsfv72H",
                256 => "FDvc1#8fjmdsa843lscvms12vdAl3!vI",
                _ => throw new ArgumentException("Invalid key size.")
            };

            byte[] encodedEncryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            return encodedEncryptionKey;
        }

        private protected string EncryptData(ICryptoTransform encryptor, string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedData = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);

            return Convert.ToBase64String(encryptedData);
        }

        private protected string DecryptData(ICryptoTransform decryptor, string data)
        {
            byte[] encryptedData = Convert.FromBase64String(data);
            byte[] decryptedData = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
