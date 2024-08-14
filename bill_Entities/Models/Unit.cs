using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Unit Name")]
        public string UnitName { get; set; }
        public string Notes { get; set; }
    }
}
