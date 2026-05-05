﻿using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;
using System.Reflection.Emit;

namespace Application.Repository.Data.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            //builder.Property(i => i.Name).UseCollation("Arabic_100_CI_AS");
            builder.Property(i => i.IsDeleted).HasDefaultValue(false);
            builder.Property(i => i.Code).HasDefaultValue(0);
            builder.Property(i => i.PlaceInHall).HasDefaultValue(0);
            builder.Property(i => i.PlaceNumber).HasDefaultValue(0);
            builder.HasOne(i => i.Class)
                .WithMany(p => p.Students)
                .HasForeignKey(i => i.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}