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
    public class StudentMonthlyCertificateSettingConfigurations : IEntityTypeConfiguration<StudentMonthlyCertificateSetting>
    {
        public void Configure(EntityTypeBuilder<StudentMonthlyCertificateSetting> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.School)
                .WithMany()
                .HasForeignKey(i => i.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Week)
                .WithMany()
                .HasForeignKey(i => i.WeekId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}