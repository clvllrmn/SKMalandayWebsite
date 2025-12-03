using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKMalandayWebsite.Models
{
    public class UserInformation
    {
        public int userID { get; set; }
      
        public string fullName { get; set; }
        public string email { get; set; }
        public string contactNo { get; set; }
        public int interest { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public DateTime? birthDate { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}