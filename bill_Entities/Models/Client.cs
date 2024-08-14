﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_Entities.Models
{
    public class Client 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [Required]
        public string Phone {  get; set; }
        [Required]
        public int Number {  get; set; }
        [Required]
        public string Address { get; set; }

    }
}
