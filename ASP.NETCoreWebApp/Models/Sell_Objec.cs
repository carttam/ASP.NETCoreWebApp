using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Sell_Objec
    {
        public int Sell_ObjecID { get; set; }
        [Key]
        public int sell_id { get; set; }
        [Key]
        public int object_id { get; set; }
        public Sell Sell { get; set; }
        public Object Object { get; set; }
    }
}
