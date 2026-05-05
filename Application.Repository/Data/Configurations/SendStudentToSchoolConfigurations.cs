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
    public class SendStudentToSchoolConfigurations : IEntityTypeConfiguration<SendStudentToSchool>
    {
        public void Configure(EntityTypeBuilder<SendStudentToSchool> builder)
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Student)
                .WithMany()
                .HasForeignKey(i => i.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.SchoolSender)
                .WithMany()
                .HasForeignKey(i => i.SchoolSenderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(i => i.SchoolReceiver)
                .WithMany()
                .HasForeignKey(i => i.SchoolReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}