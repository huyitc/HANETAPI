using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.API.ViewModel.PersonImages;
using HANNET.Data.Context;
using HANNET.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HANNET.API.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly HanNetContext _context;
        private readonly IStorageRepository _storageService;
        public PersonRepository(HanNetContext context,IStorageRepository storageRepository)
        {
            _storageService=storageRepository;
            _context = context;
        }

        public async Task<int> DeletePerson(int PersonId)
        {
            var person = await _context.Persons.FindAsync(PersonId);
            if (person == null)

                throw new Exception($"Cannot delete a person becasuse not find with PersonId: {PersonId}");

            _context.Persons.Remove(person);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePersonByAliasID(int AliasID)
        {
            var person = await _context.Persons.FindAsync(AliasID);
            if (person == null)

                throw new Exception($"Cannot delete a person becasuse not find with AliasID: {AliasID}");

            _context.Persons.Remove(person);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<PersonModels>> GetByAliasId(int AliasID)
        {
            var person = await _context.Persons.FindAsync(AliasID);
            if (person == null)
                throw new Exception("Cannot find Person with aliasID");

            var query = from ps in _context.Persons
                        where ps.AliasID == AliasID
                        select ps;
            var data = await query.Select(x => new PersonModels()
            {
                PersonId = x.PersonId,
                PersonName = x.PersonName,
                AliasID = x.AliasID,
                Title = x.Title,
                Type = x.Type
            }).ToListAsync();

            return data;
        }

        public async Task<List<PersonModels>> GetByPlaceId(int PlaceId)
        {
            var query = from ps in _context.Persons
                        join pl in _context.Places
                        on ps.PlaceId equals pl.PlaceId
                        select new {ps,pl};
            if (PlaceId > 0)
            {
                query = query.Where(x => x.pl.PlaceId == PlaceId);
            }

            var data = await query.Select(x => new PersonModels()
            {
               PersonId=x.ps.PersonId,
               PersonName=x.ps.PersonName,
               AliasID=x.ps.AliasID,
               Title = x.ps.Title,
               Type = x.ps.Type
            }).ToListAsync();

            return data;
        }

        public async Task<int> PersonRegister(PersonRegisterModels models)
        {
            var person = new Person()
            {
              PersonName = models.PersonName,
              AliasID= models.AliasId,
              PlaceId = models.PlaceId,
              Title= models.Title,
              Type= models.Type
            };
            //Save image
            if (models.File != null)
            {
                person.PersonImages = new List<PersonImage>()
                {
                    new PersonImage()
                    {
                        DateCreate = DateTime.Now,
                        FileSize = models.File.Length,
                        Path = await this.SaveFile(models.File),
                    }
                };
            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person.PersonId;
        }

        public async Task<int> PersonRegisterByUrl(PersonRegisterByUrl models)
        {
            var person = new Person()
            {
                PersonName = models.PersonName,
                AliasID = models.AliasId,
                PlaceId = models.PlaceId,
                Title = models.Title,
                Type = models.Type
            };
            //Save image
            if (models.Url != null)
            {
                person.PersonImages = new List<PersonImage>()
                {
                    new PersonImage()
                    {
                        DateCreate = DateTime.Now,
                        FileSize = models.Url.Length,
                        Path = models.Url,
                    }
                };
            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person.PersonId;
        }

        public async Task<int> UpdateImage(int PersonId, PersonImageUpdate personImages)
        {
            var personImage = await _context.PersonImages.FindAsync(PersonId);
            if (personImage == null)
                throw new Exception("Cannot find Person");

            if (personImages.File != null)
            {
                personImage.Path = await this.SaveFile(personImages.File);
                personImage.FileSize = personImages.File.Length;
            }
            _context.PersonImages.Update(personImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> updatePerson(PersonUpdateModels models)
        {
            var person = await _context.Persons.FindAsync(models.PersonId);
            if (person == null)
            {
                throw new Exception("Cannot find person");

            }
            person.PersonName = models.PersonName;
            person.Title = models.Title;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateUrl(int PersonId, PersonUrlUpdate personUrls)
        {
            var personUrl = await _context.PersonImages.FindAsync(PersonId);
            if (personUrl == null)
                throw new Exception("Cannot find Person");

            if (personUrls.Url != null)
            {
                personUrl.Path = personUrls.Url;
                personUrl.FileSize = personUrls.Url.Length;
            }
            _context.PersonImages.Update(personUrl);
            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
