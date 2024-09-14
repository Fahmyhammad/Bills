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
using System.ComponentModel.DataAnnotations.Schema;

namespace bill_Entities.ViewModel
{
    public class SalesViewModel
    {
       public SalesInvoice SalesInvoices { get; set; }


        [Key]
        public int Id { get; set; }

        public int BillsCode { get; set; }
        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Quintity Must be Greater than or equal Zero")]
        public int Quintity { get; set; }

        public decimal Total { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount Must be Greater than or equal Zero")]
        public decimal Discount { get; set; }

        public decimal NetPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "PaidUp Must be Greater than or equal Zero")]
        public decimal PaidUp { get; set; }
        public decimal TheRest { get; set; }
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public int ClientId { get; set; }
        [NotMapped]
        public Client client { get; set; }
        [Required]
        [DisplayName("Item Name")]
        public int TableItemId { get; set; }
        [NotMapped]
        public tableItem TableItem { get; set; }



        [ValidateNever]
        public IEnumerable<SelectListItem> ClientsList {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ItemsList { get; set; }

       
    }
}
