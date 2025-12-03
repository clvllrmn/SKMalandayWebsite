using System;
using System.Collections.Generic;

namespace SKMalandayWebsite.Models
{
    public class tbl_interestsModel
    {
        public int interestID { get; set; } 
        public string interestName { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public virtual ICollection<tbl_usersModel> tbl_users { get; set; }

        public tbl_interestsModel()
        {
            this.tbl_users = new HashSet<tbl_usersModel>();
        }
    }
}