﻿using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Application.Repository.Data.Configurations
{
    public class TeacherClassesConfigurations : IEntityTypeConfiguration<TeacherClasses>
    {
        public void Configure(EntityTypeBuilder<TeacherClasses> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();

            builder.HasOne(i => i.Teacher)
              .WithMany(p => p.Classes)
              .HasForeignKey(i => i.TeacherId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Subject)
              .WithMany()
              .HasForeignKey(i => i.SubjectId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Class)
              .WithMany()
              .HasForeignKey(i => i.ClassId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}