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
    public class StudentScoreEditConfigurations : IEntityTypeConfiguration<StudentScoreEdit>
    {
        public void Configure(EntityTypeBuilder<StudentScoreEdit> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.IsEdited).HasDefaultValue(false);

            builder.HasOne(u => u.Teacher)
                .WithMany()
                .HasForeignKey(u => u.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(i => i.Student)
            .WithMany()
            .HasForeignKey(i => i.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.StudentScoreData)
            .WithMany()
            .HasForeignKey(i => i.StudentScoreDataId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}