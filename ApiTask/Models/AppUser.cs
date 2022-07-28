using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ApiTask.Models
{
    public class AppUser : IdentityUser
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        public string LastName { get; set; }
        public string FirstName { get; set; }
       // public string Email { get; set; }   

        //Many-to-Many Relationship
        public IEnumerable<AppUserTask> Tasks { get; set; }

        public AppUser()
        {
                Tasks = new List<AppUserTask>();
        }
    }
}
 