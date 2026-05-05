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
    public class StudentAbsentDataConfigurations : IEntityTypeConfiguration<StudentAbsentData>
    {
        public void Configure(EntityTypeBuilder<StudentAbsentData> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.Student)
                .WithMany(i => i.Absents)
                .HasForeignKey(u => u.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Week)
                .WithMany()
                .HasForeignKey(u => u.WeekId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Teacher)
               .WithMany()
               .HasForeignKey(u => u.TeacherId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Subject)
               .WithMany()
               .HasForeignKey(u => u.SubjectId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Class)
               .WithMany()
               .HasForeignKey(u => u.ClassId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}