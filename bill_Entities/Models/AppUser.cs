using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Name {  get; set; }
        [Required]
        public string Address {  get; set; }
        public string City {  get; set; }
    }
}
