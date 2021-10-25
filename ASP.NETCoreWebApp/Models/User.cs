using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [StringLength(80)]
        public string username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DataType(DataType.Text)]
        [StringLength(80)]
        public string first_name { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [StringLength(80)]
        public string last_name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string phone_number { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [StringLength(16),MinLength(8)]
        public string password {  get; set; }
        public Token token { get; set; }
        public ICollection<Sell> Sells { get; set; }
    }
}
