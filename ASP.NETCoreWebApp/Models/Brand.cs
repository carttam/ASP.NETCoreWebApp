using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        [DataType(DataType.Text)]
        public string name { get; set; }
        [DataType(DataType.Url)]
        public string website { get; set; }
        [DataType(DataType.ImageUrl)]
        public string icon { get; set; }
        public ICollection<Object> Object { get; set; }
    }
}
