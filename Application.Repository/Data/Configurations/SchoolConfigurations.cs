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
    public class SchoolConfigurations : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.index).HasDefaultValue(3);
            builder.HasOne(u => u.Administration)
                .WithMany(p => p.Schools)
                .HasForeignKey(u => u.AdministrationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}