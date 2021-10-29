using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class Muayene
    {
        
        
        [Key]
        public int muayene_id { get; set; }
        public string saati { get; set; }
        public bool muayene_durumu { get; set; }
        public virtual Hasta Hasta { get; set; }
        public virtual Doktor Doktor { get; set; }
        public ilaclar ilaclars { get; set; }
       
      

    }
}
