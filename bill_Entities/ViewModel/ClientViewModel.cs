using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.ViewModel
{
    public class ClientViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "No more than 11 digits")]
        [MinLength(11, ErrorMessage = "Not less than 11 digits")]
        public string Phone { get; set; }
        [Required]
       
        public int Number { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
