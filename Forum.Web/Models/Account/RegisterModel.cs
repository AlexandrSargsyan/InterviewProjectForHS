using Forum.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Nickname Required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",ErrorMessage = "Only letters and digits")]
        [MaxLength(50, ErrorMessage = "Nickname must be max 50 letter")]
        [IsUserUnique(ErrorMessage = "Nickname must be unique")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$",
            ErrorMessage = "Password must have one number, one lowercase , one uppercase and one special character and must have 8-16 symbols")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Confirm password must match with password field")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="City Required")]
        [MaxLength(100, ErrorMessage = "City must be max 100 Letters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country Required")]
        [MaxLength(100,ErrorMessage = "Contry must be max 100 Letters")]
        public string Country { get; set; }
    }
}