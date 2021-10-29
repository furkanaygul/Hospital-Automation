using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class ilaclar
    {
        [Key]
        public int ilac_id { get; set; }
        public string ilac_adi { get; set; }
        public string kullanim_zamani { get; set; }
        public int muayene_id { get; set; }
        public ICollection<Muayene> Muayene { get; set; }

    }
}
