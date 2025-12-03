using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SKMalandayWebsite.Models;


namespace SKMalandayWebsite.Models.Maps
{
    public class tbl_usersMap : EntityTypeConfiguration<tbl_usersModel>

    {
        public tbl_usersMap ()
        {
            HasKey(i => i.userID);
            ToTable("tbl_users");
        }
    }
}