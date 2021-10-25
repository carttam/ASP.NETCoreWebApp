using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class DisCountCode
    {
        public int DisCountCodeID { get; set; }
        [DataType(DataType.Text)]
        public string code { get; set; }
        [DataType(DataType.Date)]
        public DateTime gen_date { get; set; }
        [DataType(DataType.Date)]
        public DateTime exp_date { get; set; }
        [Range(0,100)]
        public int discount { get; set; }
        public ICollection<Sell> Sells { get; set; }
    }
}
