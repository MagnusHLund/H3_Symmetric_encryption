namespace H3_Symmetric_encryption.Interfaces.Controllers
{
    public interface IFileController
    {
        Task SaveFileAsync(string data);
        Task CreateNewFileAsync();
        Task<string> LoadFileAsync();
        void DeleteFile();
    }
}