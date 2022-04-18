using Microsoft.AspNetCore.Http;
using System;

namespace HANNET.API.ViewModel.PersonImages
{
    public class PersonImageUpdate
    {
        public IFormFile File { get; set; }
    }
}
