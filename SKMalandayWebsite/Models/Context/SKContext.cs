using SKMalandayWebsite.Models.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SKMalandayWebsite.Models.Context
{
    public class SKContext : DbContext
    {
        static SKContext ()
        {
            Database.SetInitializer<SKContext>(null);
        }

        public SKContext(): base("Name=sk_db") { }

        public virtual DbSet<tbl_usersModel> tbl_users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_usersMap());
           


;        }

    }
}