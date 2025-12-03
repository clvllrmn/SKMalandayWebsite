using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKMalandayWebsite.Models
{
    public class tbl_adminModel
    {
        public int adminID { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}