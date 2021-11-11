using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Object
    {
        public int ObjectID { get; set; }
        [DataType(DataType.Text)]
        public string title { get; set; }
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }
        [DataType(DataType.Currency)]
        public double price { get; set; }
        [Range(0, 100)]
        public int discount { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public int? AttributeID { get; set; }
        public Attribute Attribute { get; set; }
        [Required]
        public SubCategori SubCategori { get; set; }
        public ICollection<Sell> Sells{ get; set; }
    }
}
