using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SafetyPanel.Models.Admin
{
    public class Adminler
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(35)]
        public string FullName { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(15)]
        public string UserName { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(25)]
        public string Email { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Password { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Salt { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(10)]
        public string Reset_Password_Code { get; set; }

        [Column(TypeName = "Int")]
        public int IsDeleted { get; set; }

        [Column(TypeName = "Int")]
        public int AccountVerify { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(100)]
        public string Photo { get; set; }
    }
}