﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Views.Other;
using Application.Core.Views.Parent;
using Application.Core.Views.Reports;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public ParentService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        public async Task<StudentProfile> GetStudentProfile(long StudentId)
        {
            var student = await context.Student.AsNoTracking()
                                                  .Include(p => p.Absents)
                                                  .Include(p => p.AbsentCases)
                                                  .Include(p => p.Class)
                                                  .FirstOrDefaultAsync(p => p.Id == StudentId);
            var Weeks = await context.Week.AsNoTracking()
                                         .Select(p => new
                                         {
                                             p.Id,
                                             p.Name,
                                             p.StartDate,
                                             p.EndDate
                                         })
                                         .ToListAsync();
            var CurrentWeekId = Weeks.First(p => p.StartDate <= DateOnly.FromDateTime(DateTime.Now) && p.EndDate >= DateOnly.FromDateTime(DateTime.Now)).Id;
            if (student == null) throw new Exception("خطأ في تحديد البيانات بالرجاء الرجوع الي شئون الطلاب بالمدرسة");
            var Data = new StudentProfile();
            Data.Code = student.Code;
            Data.Name = student.Name;
            Data.ClassFullId = student.Class.FullId;
            foreach (var abs in student.AbsentCases)
            {
                Data.KnowAbsent.Add(new KnowAbsentData()
                {
                    From = DateHelper.GetDateString(DateTime.Parse(abs.StartDate.ToString())),
                    To = DateHelper.GetDateString(DateTime.Parse(abs.EndDate.ToString())),
                });
            }
            var stepabsent = new List<long>();
            foreach (var abs in student.Absents.Where(p => p.IsAbsent && p.ClassNumber == 1).ToList())
            {
                if (student.AbsentCases.Any(p => p.StartDate <= abs.Date && p.EndDate >= abs.Date)) continue;
                stepabsent.Add(abs.WeekId);
            }
            foreach (var week in stepabsent.DistinctBy(p => p))
            {
                Data.Absent.Add(new AbsentData()
                {
                    WeekName = Weeks.First(p => p.Id == week).Name,
                    Absent = stepabsent.Count(p => p == week)
                });
                if (CurrentWeekId == week)
                    Data.WeekAbsent = stepabsent.Count(p => p == week);
            }
            Data.AllAbsent = Data.Absent.Sum(p => p.Absent);
            return Data;
        }
        public async Task<TotalStudentAvrage> GetStudentAvrageView(long StudentId)
        {
            var StudentGPA = await context.StudentClassGPA.Where(p => p.StudentId == StudentId && p.Subject.Status != "Off")
                                                          .Include(p => p.Week)
                                                          .Include(p => p.Subject)
                                                          .Include(p => p.Class)
                                                          .OrderBy(p => p.Subject.Index)
                                                          .Select(p => new
                                                          {
                                                              StudentId = p.Student.Id,
                                                              ClassFullId = p.Class.FullId,
                                                              SubjectName = p.Subject.Name,
                                                              p.Review,
                                                              p.Behavior,
                                                              p.Homework,
                                                              p.Activity,
                                                              p.Missions,
                                                              p.Oral,
                                                              p.Classwork,
                                                              Total = p.TotalRate,
                                                              p.FirstExam,
                                                              p.SecondExam,
                                                              WeekName = p.Week.Name
                                                          })
                                                          .AsNoTracking()
                                                          .ToListAsync();
            if (StudentGPA == null || StudentGPA.Count() == 0) throw new Exception("لم يتم تسجيل الدرجات بعد");
            var result = new TotalStudentAvrage();
            result.Level = HelperFn.GetLevel(StudentGPA.First().ClassFullId);
            result.ClassFullId = StudentGPA.First().ClassFullId;
            foreach (var Student in StudentGPA)
            {
                result.Subjects.Add(new TotalSubjectsAvrageDetails()
                {
                    SubjectsName = Student.SubjectName,
                    Activity = Student.Activity,
                    Behavior = Student.Behavior,
                    Homework = Student.Homework,
                    Classwork = Student.Classwork,
                    Missions = Student.Missions,
                    Oral = Student.Oral,
                    Review = Student.Review,
                    Total = Student.Total,
                    Exam1 = Student.FirstExam,
                    Exam2 = Student.SecondExam,
                    WeekName = Student.WeekName
                });
            }
            return result;
        }
        public async Task<List<StudentPunchmentsData>> GetStudentPunchments(long StudentId)
        {
            var Data = new List<StudentPunchmentsData>();
            var pun = await context.Punchments.Where(p => p.StudentId == StudentId)
                                              .AsNoTracking()
                                              .Select(p => new
                                              {
                                                  p.SendDate,
                                                  p.Id,
                                                  p.ConfirmedDate,
                                                  p.IsConfirmed,
                                                  p.IsSeen,
                                                  p.Header,
                                                  p.Body
                                              })
                                              .OrderBy(p => p.IsSeen)
                                              .ThenBy(p => p.SendDate)
                                              .ToListAsync();
            foreach (var p in pun)
            {
                Data.Add(new StudentPunchmentsData()
                {
                    Body = p.Body,
                    Header = p.Header,
                    IsConfirmed = p.IsConfirmed,
                    Id = p.Id,
                    IsViewed = p.IsSeen,
                    ReportDate = DateHelper.GetDateString(DateTime.Parse(p.SendDate.ToString()))
                });
            }
            return Data;
        }
        public async Task<ErrorResponce> ConfirmStudentPunchments(long PunchmentId)
        {
            var pun = await context.Punchments.FirstOrDefaultAsync(p => p.Id == PunchmentId);
            if (pun == null) throw new Exception("تاكد من ادخال جميع البيانات");
            pun.SeenDate = DateOnly.FromDateTime(DateTime.Now);
            pun.IsSeen = true;
            unitOfWork.Repository<Punchments, long>().Update(pun);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> SendComplent(SendComplentView view)
        {
            await unitOfWork.Repository<StudentComplents, long>().AddAsync(new StudentComplents()
            {
                StudentId = view.StudentId,
                Body = view.Body,
                SchoolId = view.SchoolId,
                Header = view.Header,
                IsConfirmed = false,
                SendDate = DateOnly.FromDateTime(DateTime.Now)
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تقديم الشكوي للمدرسة");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<List<StudentComplentData>> GetStudentComplents(long StudentId)
        {
            var Data = new List<StudentComplentData>();
            var pun = await context.StudentComplents.Where(p => p.StudentId == StudentId)
                                              .AsNoTracking()
                                              .Select(p => new
                                              {
                                                  p.Id,
                                                  p.SendDate,
                                                  p.ConfirmedDate,
                                                  p.IsConfirmed,
                                                  p.Body
                                              })
                                              .OrderBy(p => p.IsConfirmed)
                                              .ThenBy(p => p.SendDate)
                                              .ToListAsync();
            foreach (var p in pun)
            {
                Data.Add(new StudentComplentData()
                {
                    Body = p.Body,
                    IsConfirmed = p.IsConfirmed,
                    Id = p.Id,
                    SendDate = DateHelper.GetDateString(DateTime.Parse(p.SendDate.ToString())),
                    ConfirmedDate = p.ConfirmedDate == null ? "" : DateHelper.GetDateString(DateTime.Parse(p.ConfirmedDate.ToString())),
                });
            }
            return Data;
        }

    }
}