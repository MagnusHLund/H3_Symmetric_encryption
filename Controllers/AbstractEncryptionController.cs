namespace H3_Symmetric_encryption.Controllers
{
    public abstract class AbstractEncryptionController
    {
        private protected byte[] GenerateIv()
        {
            throw new NotImplementedException();
        }

        private protected byte[] GetEncryptionKey()
        {
            throw new NotImplementedException();
        }
    }
}