using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankMVCWeb.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength (16)]
        public string CadrID { get; set; }
        [Required]
        [StringLength(4)]
        public string PinCode { get; set; }
  
    }
}