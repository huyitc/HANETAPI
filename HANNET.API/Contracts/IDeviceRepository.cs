using HANNET.API.ViewModel;
using HANNET.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HANNET.API.Contracts
{
    public interface IDeviceRepository
    {
        public Task<List<DeviceModels>> GetAll();

        public Task<List<DeviceModels>> GetByPlaceID(int PlaceId);

        public Task<int> Update(DeviceUpdateModels models);

        public Task<Device> CreateDevice(DeviceAddModels models);

        public Task<int> Delete(int DeviceId);
    }
}
