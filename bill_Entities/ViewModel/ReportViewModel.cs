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
    public class ReportViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Report From")]
        public DateTime ReportFrom { get; set; }
        [Required]
        [DisplayName("Report To")]
        public DateTime ReportTo { get; set; }

        [Required]
        public int SalesId { get; set; }
        public SalesInvoice Sales { get; set; }

        [ValidateNever]
      public IEnumerable<SelectListItem> SalesList { get; set; }
        public Report Report { get; set; }

    }
}
