using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData
            (
            new Car
            {
                Id = new Guid("70fa5138-5fb2-489b-ab3f-5c094a6633af"),
                Car_Name = "Гоночная супер тачка",
                DollarCost=30000,
                Engine_Id = new Guid("4cfd7567-cf3e-4401-a7e4-fd552028dba1")


            },
            new Car
            {
                Id = new Guid("20c7ee04-ff7b-414d-9433-ba7ea16cc570"),
                Car_Name = "Не гоночная супер тачка",
                DollarCost=15000,
                Engine_Id = new Guid("76da5764-b3df-4457-a0bb-29b132fe6c21")

            }) ;

        }
    }
}
