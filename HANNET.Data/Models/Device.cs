using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANNET.Data.Models
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
