using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace autobook.Core.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Make>().HasData(
                new Make{
                    Id = 1,
                    Name = "Make1",
                },
                new Make{
                    Id = 2,
                    Name = "Make2",
                },
                new Make{
                    Id = 3,
                    Name = "Make3",
                }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model{
                    Id = 1,
                    Name = "Model1",
                    MakeId = 1
                },
                new Model{
                    Id = 2,
                    Name = "Model2",
                    MakeId = 2
                },
                new Model{
                    Id = 3,
                    Name = "Model3",
                    MakeId = 3
                }
            );

            modelBuilder.Entity<Feature>().HasData(
                new Feature{
                    Id = 1,
                    Name = "Feature1"
                },
                new Feature{
                    Id = 2,
                    Name = "Feature2"
                },
                new Feature{
                    Id = 3,
                    Name = "Feature3"
                }
            );
        }
    }
}