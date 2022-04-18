using HANNET.API.ViewModel;
using HANNET.API.ViewModel.PersonImages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IPersonRepository
    {
      public Task<int> PersonRegister(PersonRegisterModels models);
      public Task<int> PersonRegisterByUrl(PersonRegisterByUrl models);

      public Task<List<PersonModels>> GetByPlaceId(int PlaceId);

      public Task<int> UpdateImage(int PersonId,PersonImageUpdate personImages);

      public Task<int> UpdateUrl(int PersonId, PersonUrlUpdate personUrls);
      public Task<List<PersonModels>> GetByAliasId(int AliasID);
      public Task<int> DeletePerson(int PersonId);
      public Task<int> DeletePersonByAliasID(int AliasID);
      public Task<int>updatePerson(PersonUpdateModels models);

    }
}
