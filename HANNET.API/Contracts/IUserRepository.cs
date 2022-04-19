using HANNET.API.ViewModel.User;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IUserRepository 
    {
      public Task<string> Authenticate(LoginModels models);
      public Task<bool> Register(RegisterModels models); 
    }
}
