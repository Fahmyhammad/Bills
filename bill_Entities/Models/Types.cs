using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class Types
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        public string Notes { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
    }
}
