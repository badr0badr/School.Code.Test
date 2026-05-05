﻿using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Application.Repository.Data.Configurations
{
    public class TeacherConfigurations : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.CanAccesControl).HasDefaultValue(false);
            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Role2)
                .WithMany()
                .HasForeignKey(u => u.RoleId2)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.School)
                .WithMany(p => p.Teachers)
                .HasForeignKey(i => i.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.TeacherTitle)
                .WithMany()
                .HasForeignKey(i => i.TeacherTitleId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.MainSubject)
                .WithMany()
                .HasForeignKey(i => i.MainSubjectId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}