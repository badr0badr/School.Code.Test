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
    public class DailySupervisionConfigurations : IEntityTypeConfiguration<DailySupervision>
    {
        public void Configure(EntityTypeBuilder<DailySupervision> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.Class)
                .WithMany()
                .HasForeignKey(u => u.ClassId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.TSaturday)
                .WithMany()
                .HasForeignKey(u => u.Saturday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.TSunday)
                .WithMany()
                .HasForeignKey(u => u.Sunday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.TMonday)
                .WithMany()
                .HasForeignKey(u => u.Monday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.TTuseday)
                .WithMany()
                .HasForeignKey(u => u.Tuseday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.TWednesday)
                .WithMany()
                .HasForeignKey(u => u.Wednesday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.TThursday)
                .WithMany()
                .HasForeignKey(u => u.Thursday)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.School)
                .WithMany()
                .HasForeignKey(u => u.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}