using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASP.NETCoreWebApp.Models
{
    public class Attribute
    {
        public int AttributeID { get; set; }
        [DataType(DataType.MultilineText)]
        public string titles { get; set; }
        [DataType(DataType.MultilineText)]
        public string datas { get; set; }
        public Object Object { get; set; }
    }
}
