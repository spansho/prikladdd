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
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.HasData
            (
            new Engine
            {
                Id = new Guid("4cfd7567-cf3e-4401-a7e4-fd552028dba1"),
                EngineName = "v8 tank",
                EngineHorsePower = 650,
                MiliageLimitKm=200000,


            },
            new Engine
            {
                Id = new Guid("76da5764-b3df-4457-a0bb-29b132fe6c21"),
                EngineName = "v12 track",
                EngineHorsePower = 1000,
                MiliageLimitKm = 30000
            });
          
        }
    }
}
