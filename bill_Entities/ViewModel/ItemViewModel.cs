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
    public class ItemViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Not less than 3 digits")]
        public string ItemName { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "SELLING PRICE Must be Greater than or equal Zero")]
        [DisplayName("Selling Price")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "BUYING PRICE Must be Greater than or equal Zero")]
        [DisplayName("Buying Price")]
        public decimal BuyingPrice { get; set; }
        public string Notes { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public int CompanyId { get; set; }
        public Company company { get; set; }
        [Required]
        [DisplayName("Type Name")]
        public int TypeId { get; set; }
        public Types type { get; set; }



        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public tableItem item { get; set; }

    }
}
