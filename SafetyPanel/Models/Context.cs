using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SafetyPanel.Models
{
    public class Context : DbContext
    {
        public DbSet<Admin.Adminler> Admins { get; set; }
        public DbSet<Adres.Province> Provinces { get; set; }
        public DbSet<Adres.District> Districts { get; set; }
    }
}