using HANNET.API.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IDeviceRepository
    {
        Task<List<DeviceModels>> GetAll();

        Task<List<DeviceModels>> GetByPlaceID(int PlaceId);

        Task<int> Update(DeviceUpdateModels models);
    }
}
