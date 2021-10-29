using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class Doktor
    {
        [ForeignKey("Oda")]
        public int id { get; set; }
        public string adi_soyadi { get; set; }
        public string unvani { get; set; }
        public virtual Oda Oda { get; set; }

    }
}
