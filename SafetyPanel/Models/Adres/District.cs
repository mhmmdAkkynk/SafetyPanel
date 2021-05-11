using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SafetyPanel.Models.Adres
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(25)]
        public string DistrictName { get; set; }
        public virtual Province Province { get; set; }

    }
}