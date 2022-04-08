using System.IO;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IStorageRepository
    {
        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);
    }
}
