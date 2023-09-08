using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Registration_System.Models
{
    public class User:IdentityUser
    {     
        public string Name { get; set; }
    }
}
