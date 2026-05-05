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
    public class TeacherSupervisorConfigurations : IEntityTypeConfiguration<TeacherSupervisor>
    {
        public void Configure(EntityTypeBuilder<TeacherSupervisor> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.School)
                .WithMany()
                .HasForeignKey(u => u.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Subject)
               .WithMany()
               .HasForeignKey(u => u.SubjectId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Teacher)
               .WithMany()
               .HasForeignKey(u => u.TeacherId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}