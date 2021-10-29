using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class Oda
    {

        
        public int id { get; set; }
        public int oda_no { get; set; }
        public virtual Doktor Doktor { get; set; }
    }
}
