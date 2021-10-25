using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class SubCategori
    {
        public int SubCategoriID { get; set; }
        [DataType(DataType.Text)]
        public string title {  get; set; }
        [Key]
        public int categori_id { get; set; }
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }
        public Categori Categori { get; set; }
    }
}
