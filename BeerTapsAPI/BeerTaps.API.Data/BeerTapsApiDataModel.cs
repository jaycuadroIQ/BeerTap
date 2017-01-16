using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.SqlServer.Server;

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
            InitializeConnectionStringParameters();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BeerTapsApiDataModel, Migrations.Configuration>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                
        }

        public DbSet<Office> OfficesData { get; set; }
        public DbSet<Tap> TapsData { get; set; }

        private void InitializeConnectionStringParameters()
        {
            //string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            const string BASE_PROJECT = "BeerTapsAPI.WebApi";
            const string DATA_PROJECT = "BeerTaps.API.Data";

            string path = AppDomain.CurrentDomain.BaseDirectory.Replace(BASE_PROJECT, DATA_PROJECT);
            path = string.Concat(path, "database\\");

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            
        }

    }
}
