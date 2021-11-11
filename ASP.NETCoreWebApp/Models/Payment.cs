using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Payment
    {
        public int PaymentID { get; set; } 
        public int UserID { get; set; }
        [DataType(DataType.Currency)]
        public double? total_price { get; set; }
        public string paymentCode { get; set; }
        public int? discountCodeID { get; set; }
        public ICollection<Sell> Sells { get; set; }
        public DisCountCode DisCountCode { get; set; }
        public User User { get; set; }
    }
}
