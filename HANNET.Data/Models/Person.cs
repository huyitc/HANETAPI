using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int AliasID { get; set; }
        public string Title { get; set; }
        public int PlaceID { get; set; }
        public Place Place { get; set; }
    }
}
