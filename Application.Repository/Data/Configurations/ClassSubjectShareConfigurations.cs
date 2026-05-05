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
    public class ClassSubjectShareConfigurations : IEntityTypeConfiguration<ClassSubjectShare>
    {
        public void Configure(EntityTypeBuilder<ClassSubjectShare> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.ShareAmount).HasDefaultValue(0);
            builder.HasOne(i => i.Subject)
            .WithMany(p => p.ClassSubjectShares)
            .HasForeignKey(i => i.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}