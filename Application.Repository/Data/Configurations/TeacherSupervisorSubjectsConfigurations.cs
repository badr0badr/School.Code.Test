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
    public class TeacherSupervisorSubjectsConfigurations : IEntityTypeConfiguration<TeacherSupervisorSubjects>
    {
        public void Configure(EntityTypeBuilder<TeacherSupervisorSubjects> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Teacher)
                .WithMany(p => p.SubSubjects)
                .HasForeignKey(i => i.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Subject)
                .WithMany()
                .HasForeignKey(i => i.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}