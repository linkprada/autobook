using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autobook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace autobook.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Seed();

            modelBuilder.Entity<VehiculeFeature>()
                .HasKey(vf => new {vf.VehiculeId , vf.FeatureId});
        }

        
    }
}