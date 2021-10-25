using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Categori
    {
        public int CategoriID { get; set; }
        [DataType(DataType.Text)]
        public string title { get; set; }
        public ICollection<SubCategori> subCategoris { get; set; }
    }
}
