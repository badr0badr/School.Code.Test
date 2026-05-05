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
    public class TeacherTitleConfigurations : IEntityTypeConfiguration<TeacherTitle>
    {
        public void Configure(EntityTypeBuilder<TeacherTitle> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.ShareAmount1_2).HasDefaultValue(0);
            builder.Property(i => i.ShareAmount3).HasDefaultValue(0);
            builder.Property(i => i.ShareAmount4).HasDefaultValue(0);
        }
    }
}