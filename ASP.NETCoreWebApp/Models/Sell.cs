using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Sell
    {
        public int SellID { get; set; }
        public int ObjectID { get; set; }
        public int UserID { get; set; }
        public int? PaymentID { get; set; }
        public Object Object { get; set; }
        public Payment Payment { get; set; }
        public User User { get; set; }
    }
}
