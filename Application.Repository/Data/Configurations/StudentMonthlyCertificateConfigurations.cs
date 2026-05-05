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
    public class StudentMonthlyCertificateConfigurations : IEntityTypeConfiguration<StudentMonthlyCertificate>
    {
        public void Configure(EntityTypeBuilder<StudentMonthlyCertificate> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Class)
                .WithMany()
                .HasForeignKey(i => i.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.Student)
                .WithMany()
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Subject)
                .WithMany()
                .HasForeignKey(i => i.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}