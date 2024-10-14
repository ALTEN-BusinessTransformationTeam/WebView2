using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ReactWebView2_Template.Models
{


    public class Context : DbContext
    {

        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            //options.UseSqlite($"Data Source={DbPath}");
            options.UseSqlServer("server=PF1620YE-A098\\SQLEXPRESS;database=TemplateDb;trusted_connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true;");
        }

        public void Intitialize()
        {
            var ordenes = new List<Orden>
            {
                new Orden{Codigo="03HZJ0001",Site="Sevilla",FechaIni=DateTime.Parse("2024-09-01")},

            };

            ordenes.ForEach(i => this.Ordenes.Add(i));
            this.SaveChanges();

            var operaciones = new List<Operacion>
            {
                new Operacion{Orden=ordenes[0],PN="6M00800400",Estado="Abierta",},

            };
            operaciones.ForEach(i => this.Operaciones.Add(i));
            this.SaveChanges();

        }      

    }
}
