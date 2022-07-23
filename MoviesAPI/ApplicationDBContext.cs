using Microsoft.EntityFrameworkCore;
using MoviesAPI.Configurations.Entities;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new GenresConfigurations());
        }
    }
}
