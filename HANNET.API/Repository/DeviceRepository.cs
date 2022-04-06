using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.Data.Context;
using HANNET.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HANNET.API.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly HanNetContext _context;
        public DeviceRepository(HanNetContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceModels>> GetAll()
        {
            var query = from dv in _context.Devices
                        join pl in _context.Places
                        on dv.PlaceId equals pl.PlaceId
                        select new {pl,dv};
            var data = await query.Select(x => new DeviceModels()
            {
                Address = x.pl.Address,
                DeviceId = x.dv.DeviceId,
                DeviceName = x.dv.DeviceName,
                PlaceName = x.pl.PlaceName

            }).ToListAsync();

            return data;
        }

        public async Task<List<DeviceModels>> GetByPlaceID(int PlaceId)
        {
            var query = from dv in _context.Devices
                        join pl in _context.Places
                        on dv.PlaceId equals pl.PlaceId
                        select new { pl, dv };
           if(PlaceId >0)
            {
                query = query.Where(x => x.pl.PlaceId == PlaceId);
            }

            var data = await query.Select(x => new DeviceModels()
            {
                Address = x.pl.Address,
                DeviceId = x.dv.DeviceId,
                DeviceName = x.dv.DeviceName,
                PlaceName = x.pl.PlaceName
            }).ToListAsync();

            return data;
        }

        public async Task<int> Update(DeviceUpdateModels models)
        {
            var device = await _context.Devices.FindAsync(models.DeviceId);
            if (device == null)
            {
                throw new Exception("Cannot find device");
               
            }
            device.DeviceName = models.DeviceName;
            return await _context.SaveChangesAsync();
        }
    }
 }

