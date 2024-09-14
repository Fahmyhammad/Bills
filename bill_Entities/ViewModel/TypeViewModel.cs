using bill_Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.ViewModel
{
    public class TypeViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Not less than 3 digits")]
        public string TypeName { get; set; }
        public string Notes { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public Types type { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
