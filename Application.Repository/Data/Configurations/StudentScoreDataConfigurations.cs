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
    public class StudentScoreDataConfigurations : IEntityTypeConfiguration<StudentScoreData>
    {
        public void Configure(EntityTypeBuilder<StudentScoreData> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Review).HasDefaultValue(0);
            builder.Property(i => i.Behavior).HasDefaultValue(0);
            builder.Property(i => i.Homework).HasDefaultValue(0);
            builder.Property(i => i.Activity).HasDefaultValue(0);
            builder.Property(i => i.Missions).HasDefaultValue(0);
            builder.Property(i => i.Oral).HasDefaultValue(0);
            builder.Property(i => i.Classwork).HasDefaultValue(0);

            builder.HasOne(i => i.Subject)
            .WithMany()
            .HasForeignKey(i => i.SubjectId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Teacher)
            .WithMany()
            .HasForeignKey(i => i.TeacherId)
            .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(i => i.Week)
            .WithMany()
            .HasForeignKey(i => i.WeekId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Class)
            .WithMany()
            .HasForeignKey(i => i.ClassId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Student)
            .WithMany(p => p.Scores)
            .HasForeignKey(i => i.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}