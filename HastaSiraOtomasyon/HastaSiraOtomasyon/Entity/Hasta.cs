using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class Hasta
    {
        [Key]
        public Int64 tc { get; set; }
        public string adi_soyadi { get; set; }
        public string dogum_tarihi { get; set; }
        public string resmi { get; set; }
    }
}
