using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace BeerTapsAPI.Data
{
    public class MasterData : DbContext
    {
        public MasterData() : base()
        {

        }

        public DbSet<OfficeDto> OfficesData { get; set; }
        public DbSet<TapDto> TapsData { get; set; }
    }
}

