using System;

namespace ApiTask.DTOs
{
    public class UserDetailsToReturnDto
    {
        public string Id { get; set; } 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
