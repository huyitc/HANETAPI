using Microsoft.AspNetCore.Identity;
using System;

using System.ComponentModel.DataAnnotations;


namespace HANNET.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }

    }
}
