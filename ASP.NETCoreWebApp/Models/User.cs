using ASP.NETCoreWebApp.Manager;
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
        [Display(Name ="First Name")]
        public string first_name { get; set; }
        [DataType(DataType.Text)]
        [Required]
        [StringLength(80)]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string address { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "Not a valid phone number")]
        [Required]
        [Display(Name = "Phone Number")]
        public string phone_number { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [StringLength(256), MinLength(8),MaxLength(16)]
        public string password { get; set; }
        public Token token { get; set; }
        public ICollection<Payment> Payments{ get; set; }
        public ICollection<Sell> Sells { get; set; }
    }
}
