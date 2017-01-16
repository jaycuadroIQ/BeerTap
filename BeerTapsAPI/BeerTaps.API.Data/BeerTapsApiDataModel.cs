using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace BeerTapsAPI.Data
{
    using System.Data.Entity;
    using System.Linq;
    using BeerTapsAPI.Model;
    using System.Collections.Generic;

    public partial class BeerTapsApiDataModel : DbContext
    {
        public BeerTapsApiDataModel()
            : base("name=BeerTapsApiDataModel")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Office> OfficesData { get; set; }
        public DbSet<Tap> TapsData { get; set; }
       
    }
}
