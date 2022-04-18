using System.IO;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IStorageRepository
    {
       public string GetFileUrl(string fileName);

       public Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

      public  Task DeleteFileAsync(string fileName);
    }
}
