using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.User
{
    public class UserRegister
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "FullName is null")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "EmailId is null")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[0-9a-zA-Z]+[.+-_]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3}){0,1}", 
            ErrorMessage = "EmailId is not valid")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is null")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mobile Number is null")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "IsAdmin is null")]
        public string IsAdmin { get; set; }
    }
}
