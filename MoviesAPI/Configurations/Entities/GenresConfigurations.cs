using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Configurations.Entities
{
    public class GenresConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre { 
                    Id = 1,
                    Name = "Action"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Comedy"
                });
        }
    }
}
