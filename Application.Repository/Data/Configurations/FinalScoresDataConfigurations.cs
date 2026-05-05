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
    public class FinalScoresDataConfigurations : IEntityTypeConfiguration<FinalScoresData>
    {
        public void Configure(EntityTypeBuilder<FinalScoresData> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(u => u.Student)
                .WithMany()
                .HasForeignKey(u => u.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.Subject)
               .WithMany()
               .HasForeignKey(u => u.SubjectId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.Class)
               .WithMany()
               .HasForeignKey(u => u.ClassId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}