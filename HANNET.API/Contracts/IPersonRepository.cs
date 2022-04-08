using HANNET.API.ViewModel;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IPersonRepository
    {
        Task<int> Register(PersonRegisterModels models);

        Task<PersonModels> GetById(int PersonId);
    }
}
