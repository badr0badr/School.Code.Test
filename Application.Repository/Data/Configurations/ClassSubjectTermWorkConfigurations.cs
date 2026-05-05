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
    public class ClassSubjectTermWorkConfigurations : IEntityTypeConfiguration<ClassSubjectTermWork>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectTermWork> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.Week)
                .WithMany()
                .HasForeignKey(u => u.WeekId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}