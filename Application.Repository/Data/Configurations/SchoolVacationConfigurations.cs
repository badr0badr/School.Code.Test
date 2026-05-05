﻿using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Data.Configurations
{
    public class SchoolVacationConfigurations : IEntityTypeConfiguration<SchoolVacation>
    {
        public void Configure(EntityTypeBuilder<SchoolVacation> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.School)
                .WithMany(p => p.Vacations)
                .HasForeignKey(i => i.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}