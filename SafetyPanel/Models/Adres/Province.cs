using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SafetyPanel.Models.Adres
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(35)]
        public string ProvincesName { get; set; }
        public IEnumerable<District> Districts{ get; set; }
    }
}