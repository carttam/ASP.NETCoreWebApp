using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ASP.NETCoreWebApp.Models
{
    public class Token
    {
        public int TokenID { get; set; }
        [DataType(DataType.Text)]
        public string token {  get; set; }
        [DataType(DataType.DateTime)]
        public DateTime gen_dateTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime exp_dateTime { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
