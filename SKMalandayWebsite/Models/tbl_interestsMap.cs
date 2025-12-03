using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using SKMalandayWebsite.Models;


namespace SKMalandayWebsite.Models.Maps
{
    public class tbl_interestsMap : EntityTypeConfiguration<tbl_interestsModel>

    {
        public tbl_interestsMap()
        {
            HasKey(i => i.interestID);
            ToTable("tbl_users");
        }
    }
}