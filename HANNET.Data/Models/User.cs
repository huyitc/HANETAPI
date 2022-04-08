using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; } 

        public int PlaceId { get; set; }

        public Place Place { get; set; }

    }
}
