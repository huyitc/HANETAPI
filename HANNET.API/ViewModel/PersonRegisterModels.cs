using Microsoft.AspNetCore.Http;

namespace HANNET.API.ViewModel
{
    public class PersonRegisterModels
    {
        public string PersonName { get; set; }
        public IFormFile File { get; set; }
        public int AliasId { get; set; }
        public int PlaceId { get; set; }
        public string Title { get; set; }
        public bool Type { get; set; }
    }
}
