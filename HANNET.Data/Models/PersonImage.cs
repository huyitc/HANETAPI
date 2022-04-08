using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Models
{
    public class PersonImage
    {
        public int id { get; set; }
        public int PersonId { get; set; }
        public string Path { get; set; }
        public DateTime DateCreate { get; set; }
        public long FileSize { get; set; }
        public Person Person { get; set; }
    }
}
