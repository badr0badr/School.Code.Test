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
    public class ExamCodesConfigurations : IEntityTypeConfiguration<ExamCodes>
    {
        public void Configure(EntityTypeBuilder<ExamCodes> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Student)
                .WithMany(i=>i.ExamCodes)
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Subject)
                .WithMany()
                .HasForeignKey(i => i.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.School)
                .WithMany()
                .HasForeignKey(i => i.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}