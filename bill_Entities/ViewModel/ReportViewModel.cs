using bill_Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.ViewModel
{
    public class ReportViewModel
    {
        public Report Report { get; set; }

        [ValidateNever]
      public IEnumerable<SelectListItem> SalesList { get; set; }
    }
}
