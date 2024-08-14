using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bills.Entities.Models
{
    public class Compnay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Notes {  get; set; }
    }
}
