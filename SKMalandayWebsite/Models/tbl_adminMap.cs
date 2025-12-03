using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace SKMalandayWebsite.Models
{
    public class tbl_adminMap : EntityTypeConfiguration<tbl_adminModel>

    {
        public tbl_adminMap()
        {
            HasKey(i => i.adminID);
            ToTable("tbl_admin");
        }
    }
}