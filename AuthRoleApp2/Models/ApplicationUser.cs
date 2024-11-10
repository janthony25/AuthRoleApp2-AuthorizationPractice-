﻿using Microsoft.AspNetCore.Identity;

namespace AuthRoleApp2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }    
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
