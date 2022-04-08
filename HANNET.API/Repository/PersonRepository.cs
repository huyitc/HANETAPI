using HANNET.API.Contracts;
using HANNET.API.ViewModel;
using HANNET.Data.Context;
using HANNET.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<PersonModels> GetById(int PersonId)
        {
            var person = await _context.Persons.FindAsync(PersonId);
            var placeModels = new PersonModels()
            {
                PersonId=person.PersonId,
                PersonName=person.PersonName,
                AliasID=person.AliasID,
                Title=person.Title,
                Type=person.Type,
            };
            return placeModels;
        }

        public async Task<int> Register(PersonRegisterModels models)
        {
            var person = new Person()
            {
                PersonName = models.PersonName,
                AliasID = models.AliasId,
                PlaceId = models.PlaceId,
                Title = models.Title,
                Type = models.Type,
            };
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

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
