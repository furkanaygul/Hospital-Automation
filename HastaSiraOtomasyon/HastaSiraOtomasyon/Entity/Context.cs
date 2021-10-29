using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaSiraOtomasyon.Entity
{
    public class Context : DbContext
    {
        public Context() : base("Name=Context")
        {

        }

        public DbSet<Doktor> Doktors { get; set; }
        public DbSet<Hasta> Hastas { get; set; }
        public DbSet<ilaclar> Ilaclars { get; set; }
        public DbSet<Muayene> Muayenes { get; set; }
        public DbSet<Oda> Odas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            ////modelBuilder.Entity<Oda>()
            ////            .HasOptional(d => d.Doktor) 
            ////            .WithRequired(ad => ad.Oda);

            ////modelBuilder.Entity<Muayene>()
            ////    .HasRequired(s => s.Doktor);
            modelBuilder.Entity<Hasta>()
                .Property(f => f.tc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);



        }


    }
}






