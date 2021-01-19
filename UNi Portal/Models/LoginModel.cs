using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNi_Portal.Models
{
    public class LoginModel
    {
        [Display( Name = "User ID" )]
        [Required( ErrorMessage = "User ID is required." )]
        [MinLength( 11 )]
        [MaxLength( 11 )]
        public string txtUserID { get; set; }


        [Display( Name = "Password" )]
        [Required( ErrorMessage = "Password is required." )]
        [MinLength( 6 )]
        [DataType( DataType.Password )]
        public string txtPassword { get; set; }
    }
}