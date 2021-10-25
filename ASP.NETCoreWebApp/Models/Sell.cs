using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Sell
    {
        public int SellID { get; set; }
        [Key]
        public int user_id { get; set; }
        [DataType(DataType.Currency)]
        public double total_price { get; set; }
        [Key]
        public int discountCode_id { get; set; }
        public User User { get; set; }
        public DisCountCode DisCountCode { get; set; }
        public ICollection<Sell_Objec> Objects { get; set; }
    }
}
