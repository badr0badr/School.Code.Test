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
    public class AdministrationsVacationConfigurations : IEntityTypeConfiguration<AdministrationsVacation>
    {
        public void Configure(EntityTypeBuilder<AdministrationsVacation> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Administration)
                .WithMany(i => i.Vacations)
                .HasForeignKey(i => i.AdministrationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}