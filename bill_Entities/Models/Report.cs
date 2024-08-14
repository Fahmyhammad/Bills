using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class Report
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
        public SalesInvoice Sales {  get; set; }

    }
}
