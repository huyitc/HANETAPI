using HANNET.API.ViewModel;
using HANNET.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IPlaceRepository
    {
     public Task<List<PlaceModels>> GetAllPlace();
        
      public Task<Place> CreatePlace(PlaceAddModels models);

      public Task<PlaceModels> GetById(int PlaceId);

      public Task<int> UpdatePlace(PlaceUpdateModels models);

      public Task<int> Delete(int PlaceId);   
    }
}
