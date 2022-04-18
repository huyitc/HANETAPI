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
    public class PlaceRepository:IPlaceRepository
    {
        private readonly HanNetContext _context;
        public PlaceRepository(HanNetContext context)
        {
            _context = context;
        }

        public async Task<Place> CreatePlace(PlaceAddModels model)
        {
            var places = new Place()
            {
                PlaceName = model.PlaceName,
                Address = model.Address,
            };
            _context.Places.Add(places);
            await _context.SaveChangesAsync();
            return places ;
        }

        public async Task<int> Delete(int PlaceId)
        {
            var place = await _context.Places.FindAsync(PlaceId);
            if (place == null) 
                
              throw new Exception($"Cannot find a place: {PlaceId}");

            _context.Places.Remove(place);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<PlaceModels>> GetAllPlace()
        {
            var query = from p in _context.Places
                        join u in _context.Users
                        on p.PlaceId equals u.PlaceId
                        select new { p, u };
            var data = await query.Select(x => new PlaceModels()
            {
                Address = x.p.Address,
                PlaceName = x.p.PlaceName,
                PlaceId = x.p.PlaceId,
                UserId=x.u.UserId,

            }).ToListAsync();

            return data;
        }

        public async Task<PlaceModels> GetById(int PlaceId)
        {
            var place = await _context.Places.FindAsync(PlaceId);
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.PlaceId == PlaceId);   
          var placeModels = new PlaceModels()
            {
                PlaceId = place.PlaceId,
                PlaceName= place.PlaceName,
                Address=place.Address,
                UserId=user.UserId
            };
            return placeModels ;
        }

        public async Task<int> UpdatePlace(PlaceUpdateModels models)
        {
            var place = await _context.Places.FindAsync(models.PlaceId);
            if (place == null)
            {
                throw new Exception("Cannot find place");

            }
            place.PlaceName = models.PlaceName;
            place.Address = models.Address;
            return await _context.SaveChangesAsync();
        }
    }
    }

