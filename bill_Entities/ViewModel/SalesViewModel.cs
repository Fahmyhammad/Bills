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
    public class SalesViewModel
    {
       public SalesInvoice SalesInvoices { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ClientsList {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ItemsList { get; set; }

       
    }
}
