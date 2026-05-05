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
    public class OutOfScoreConfigurations : IEntityTypeConfiguration<OutOfScore>
    {
        public void Configure(EntityTypeBuilder<OutOfScore> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.School)
                .WithMany()
                .HasForeignKey(u => u.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Subject)
               .WithMany()
               .HasForeignKey(u => u.SubjectId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Week)
               .WithMany()
               .HasForeignKey(u => u.WeekId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}