using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class SalesInvoice
    {
        [Key]
        public int Id { get; set; }

        public int BillsCode { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Quintity Must be Greater than or equal Zero")]
        public int Quintity {  get; set; }

        public decimal Total { get; set;}

        [Range(0, double.MaxValue, ErrorMessage = "Discount Must be Greater than or equal Zero")]
        public decimal Discount {  get; set;}
        
        public decimal NetPrice {  get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "PaidUp Must be Greater than or equal Zero")]
        public decimal PaidUp {  get; set; }
        public decimal TheRest { get; set;}
        public decimal Price { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public int ClientId {  get; set; }
        public Client client { get; set; }
        [Required]
        [DisplayName("Item Name")]
        public int TableItemId { get; set; }
        public tableItem TableItem {  get; set; }
    }
}
