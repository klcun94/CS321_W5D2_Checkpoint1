using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CS321_W5D2_BlogAPI.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // add a FullName property that returns FirstName + LastName
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Blog> Blogs { get; set; }
    }
}
