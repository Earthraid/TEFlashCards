using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Valid Email Address Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Required Field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "* Passwords Must Match")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }

        [Required(ErrorMessage = "* User Name Is Required")]
        public string UserName { get; set; }
    }
}