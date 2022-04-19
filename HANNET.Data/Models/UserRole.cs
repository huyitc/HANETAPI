using Microsoft.AspNetCore.Identity;
using System;

namespace HANNET.Data.Models
{
    public class UserRole:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
