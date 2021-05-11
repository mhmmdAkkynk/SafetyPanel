using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyPanel.Models
{
    public class AdminHayat
    {
        public IEnumerable<Admin.Adminler> AdminlerInformation { get; set; }
        public IEnumerable<Adres.Province> Provinces { get; set; }
        public IEnumerable<Adres.District> Districts { get; set; }
    }
}