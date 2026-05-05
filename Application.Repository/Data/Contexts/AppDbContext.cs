﻿using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Application.Repository.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<TeacherClasses> TeacherClasses { get; set; }
        public DbSet<Week> Week { get; set; }
        public DbSet<StudentScoreData> StudentScoreData { get; set; }
        public DbSet<StudentExamScoreData> StudentExamScoreData { get; set; }
        public DbSet<StudentAbsentData> StudentAbsentData { get; set; }
        public DbSet<StudentScoreEdit> StudentScoreEdit { get; set; }
        public DbSet<Administrations> Administrations { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<TeacherSupervisor> TeacherSupervisor { get; set; }
        public DbSet<SendStudentToSchool> SendStudentToSchool { get; set; }
        public DbSet<StudentAbsentCases> StudentAbsentCases { get; set; }
        public DbSet<SchoolVacation> SchoolVacation { get; set; }
        public DbSet<AdministrationsVacation> AdministrationsVacation { get; set; }
        public DbSet<TeacherSupervisorSubjects> TeacherSupervisorSubjects { get; set; }
        public DbSet<TeacherTitle> TeacherTitle { get; set; }
        public DbSet<ClassSubjectShare> ClassSubjectShare { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<ExamCodes> ExamCodes { get; set; }
        public DbSet<DailySupervision> DailySupervision { get; set; }
        public DbSet<GeneralSupervision> GeneralSupervision { get; set; }
        public DbSet<ClassSubjectTermWork> ClassSubjectTermWork { get; set; }
        public DbSet<StudentClassGPA> StudentClassGPA { get; set; }
        public DbSet<StudentMonthlyCertificate> StudentMonthlyCertificate { get; set; }
        public DbSet<StudentMonthlyCertificateSetting> StudentMonthlyCertificateSetting { get; set; }
        public DbSet<Punchments> Punchments { get; set; }
        public DbSet<StudentComplents> StudentComplents { get; set; }
        public DbSet<OutOfScore> OutOfScore { get; set; }
        public DbSet<FinalScoresData> FinalScoresData { get; set; }
    }
}