using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public List<Device> Device { get; set; }
        public List<Person> Person { get; set; }

    }
}
