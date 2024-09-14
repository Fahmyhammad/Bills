using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.ViewModel
{
    public class UnitViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [DisplayName("Unit Name")]
        [MaxLength(50,ErrorMessage = "No more than 50 digits")]
        [MinLength(3, ErrorMessage = "Not less than 3 digits")]
        public string UnitName { get; set; }
        public string Notes { get; set; }
    }
}
