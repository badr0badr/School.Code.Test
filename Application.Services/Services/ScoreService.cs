﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Services.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public ScoreService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        public async Task<ErrorResponce> AddScores(AddScoresView View)
        {
            if (View is null || View.StudentScores is null || View.StudentScores.Count == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(View.TeacherClassesId, false));
            var existingScore = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                WeekId = View.WeekId,
                SubjectId = TC.SubjectId,
                ClassId = TC.ClassId
            }, false));
            foreach (var item in View.StudentScores)
            {
                var data = existingScore.FirstOrDefault(p => p.StudentId == item.Id);
                if (data != null)
                {
                    data.Behavior = item.Behavior;
                    data.Review = item.Review;
                    data.Homework = item.Homework;
                    data.Activity = item.Activity;
                    data.Missions = item.Missions;
                    data.Oral = item.Oral;
                    data.Classwork = item.Classwork;
                    data.ClassId = TC.ClassId;
                    data.TeacherId = TC.TeacherId;
                    data.IsSaved = View.IsSaved.HasValue ? View.IsSaved.Value : false;
                    data.IsPreAbsent = false;
                    unitOfWork.Repository<StudentScoreData, long>().Update(data);
                }
                else
                {
                    await unitOfWork.Repository<StudentScoreData, long>().AddAsync(new StudentScoreData()
                    {
                        Homework = item.Homework,
                        Review = item.Review,
                        Behavior = item.Behavior,
                        StudentId = item.Id,
                        Classwork = item.Classwork,
                        Oral = item.Oral,
                        Missions = item.Missions,
                        Activity = item.Activity,
                        SubjectId = TC.SubjectId,
                        TeacherId = TC.TeacherId,
                        ClassId = TC.ClassId,
                        WeekId = View.WeekId,
                        IsSaved = View.IsSaved.HasValue ? View.IsSaved.Value : false,
                        IsPreAbsent = false,
                    });
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> IsClassExist(AddScoresView View)
        {
            if (View.TeacherClassesId <= 0 || View.WeekId <= 0 || View.Month <= 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdNoTrackingAsync(new TeacherClassesSpecification(View.TeacherClassesId, false));
            var existingScore = await unitOfWork.Repository<StudentScoreData, long>().GetAllNoTrackingAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                WeekId = View.WeekId,
                SubjectId = TC.SubjectId,
                ClassId = TC.ClassId,
                IsSaved = true
            }, false));
            if (existingScore != null) if (existingScore.Count() == 0) return new ErrorResponce(200, "");
            return new ErrorResponce(401, "تم تسجيل الصف مسبقأ");
        }
        public async Task<ErrorResponce> AddExamScores(AddScoresView View)
        {
            if (View is null || View.StudentScores is null || View.StudentScores.Count == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(View.TeacherClassesId));
            var existingScore = await unitOfWork.Repository<StudentExamScoreData, long>().GetAllAsync(new StudentExamScoreDataSpecification(new StudentExamScoreDataParams()
            {
                SubjectId = TC.SubjectId,
                ClassId = TC.ClassId,
                Month = View.Month
            }));
            foreach (var item in View.StudentScores)
            {
                var data = existingScore.FirstOrDefault(p => p.StudentId == item.Id);
                if (data == null) throw new Exception("خطأ في الحفظ");
                data.Behavior = item.PreAbsent ? 0 : item.Behavior;
                data.ExamResult = item.PreAbsent ? 0 : item.Review;
                data.TeacherId = TC.TeacherId;
                data.IsSaved = View.IsSaved.HasValue ? View.IsSaved.Value : false;
                unitOfWork.Repository<StudentExamScoreData, long>().Update(data);
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الدرجات بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ClassExamData> IsClassExamExist(long TeacherClassesId)
        {
            if (TeacherClassesId <= 0) throw new Exception("تاكد من ادخال جميع البيانات");
            var TC = await context.TeacherClasses.Include(p => p.Class).AsNoTracking().FirstOrDefaultAsync(p => p.Id == TeacherClassesId);
            if (TC == null) throw new Exception("تاكد من ادخال جميع البيانات");
            var ExamSetting = await context.StudentMonthlyCertificateSetting.AsNoTracking()
                                                                            .FirstOrDefaultAsync(p => p.SchoolId == TC.Class.SchoolId &&
                                                                                                    p.ClassName == TC.Class.Name &&
                                                                                                    p.StartExam &&
                                                                                                    !p.StartCertificates);
            if (ExamSetting == null) throw new Exception("لم يتم فتح رصد الإمتحانات بعد");
            var existingScore = await context.StudentExamScoreData.AsNoTracking()
                                                                  .Include(p => p.Student)
                                                                  .Where(p => p.SubjectId == TC.SubjectId &&
                                                                              p.ClassId == TC.ClassId &&
                                                                              p.Month == ExamSetting.Month && !p.IsSaved)
                                                                  .ToListAsync();
            var existingScoreCount = await context.StudentExamScoreData.AsNoTracking()
                                                                  .CountAsync(p => p.SubjectId == TC.SubjectId &&
                                                                              p.ClassId == TC.ClassId &&
                                                                              p.Month == ExamSetting.Month);

            if (existingScore.Count() == 0 && existingScoreCount > 0) throw new Exception("تم رفع الإمتحان مسبقأ");
            if (existingScore.Count() == 0) throw new Exception("سوف يتم فتح رصد الإمتحان بعد تسجيل الغياب");
            var Studentcount = await context.Student.CountAsync(p => p.ClassId == TC.ClassId);
            if (existingScore.Count() == Studentcount)
            {
                return new ClassExamData()
                {
                    Month = ExamSetting.Month,
                    MonthName = DateHelper.MonthNameAr(ExamSetting.Month),
                    Students = existingScore.Select(p => new addStudentScore()
                    {
                        Id = p.StudentId,
                        Name = p.Student.Name,
                        Review = p.ExamResult,
                        Behavior = p.Behavior,
                        PreAbsent = p.IsAbsent
                    }).OrderBy(p => p.Name).ToList()
                };
            }
            throw new Exception("تم تسجيل الصف مسبقأ");
        }
        public async Task<List<GetStudentForClassScore>> GetStudentForClassScore(StudentForClassScoreView view)
        {
            if (view.ClassId == 0 || view.WeekId == 0 || view.SubjectId == 0) throw new Exception("تاكد من ادخال جميع البيانات");
            var students = await context.Student.AsNoTracking()
                                                .Where(p => p.ClassId == view.ClassId)
                                                .Select(p => new
                                                {
                                                    p.Id,
                                                    p.Name
                                                })
                                                .OrderBy(p => p.Name)
                                                .ToListAsync();
            var week = await context.Week.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.WeekId);
            if (week == null) throw new Exception("تاكد من ادخال جميع البيانات");
            var existingScore = await context.StudentScoreData.AsNoTracking()
                                                              .Where(p => p.WeekId == view.WeekId &&
                                                                       p.SubjectId == view.SubjectId &&
                                                                       p.ClassId == view.ClassId)
                                                              .Select(p => new
                                                              {
                                                                  p.StudentId,
                                                                  p.Activity,
                                                                  p.Review,
                                                                  p.Missions,
                                                                  p.Homework,
                                                                  p.Oral,
                                                                  p.Behavior,
                                                                  p.Classwork
                                                              })
                                                              .ToListAsync();
            var StudentsAbsent = await context.StudentAbsentData.AsNoTracking()
                                                                .Where(p => p.IsAbsent &&
                                                                        p.SubjectId == view.SubjectId &&
                                                                        p.WeekId == view.WeekId &&
                                                                        p.ClassId == view.ClassId)
                                                                .ToListAsync();
            var StudentAbsentSkips = await context.StudentAbsentCases.AsNoTracking()
                                                                    .Where(p => p.Student.ClassId == view.ClassId)
                                                                    .Select(p => new
                                                                    {
                                                                        p.Id,
                                                                        p.StudentId,
                                                                        p.EndDate,
                                                                        p.StartDate
                                                                    })
                                                                    .ToListAsync();
            var result = new List<GetStudentForClassScore>();
            foreach (var student in students)
            {
                var studentdata = existingScore.FirstOrDefault(p => p.StudentId == student.Id);
                var StudentAbsent = StudentsAbsent.Where(p => p.StudentId == student.Id).ToList();
                var StudentAbsentSkip = StudentAbsentSkips.Where(p => p.StudentId == student.Id).ToList();
                bool preAbs = false;
                foreach (var SS in StudentAbsentSkip)
                {

                    StudentAbsent = StudentAbsent.Where(p => p.Date > SS.EndDate || p.Date < SS.StartDate).ToList();
                    if ((SS.StartDate >= week.StartDate && SS.StartDate <= week.EndDate) ||
                                            (SS.EndDate >= week.StartDate && SS.EndDate <= week.EndDate) ||
                                            (SS.StartDate <= week.StartDate && SS.StartDate <= week.EndDate && SS.EndDate >= week.StartDate && SS.EndDate >= week.EndDate))
                        preAbs = true;
                }
                if (studentdata is null)
                {
                    result.Add(new GetStudentForClassScore()
                    {
                        Name = student.Name,
                        Id = student.Id,
                        Absent = StudentAbsent.DistinctBy(p => p.Date).Count(),
                        PreAbsent = preAbs
                    });
                }
                else
                {
                    result.Add(new GetStudentForClassScore()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Behavior = studentdata.Behavior,
                        Homework = studentdata.Homework,
                        Review = studentdata.Review,
                        Activity = studentdata.Activity,
                        Classwork = studentdata.Classwork,
                        Missions = studentdata.Missions,
                        Oral = studentdata.Oral,
                        Absent = StudentAbsent.DistinctBy(p => p.Date).Count(),
                        PreAbsent = preAbs
                    });
                }
            }
            return result;
        }
        public async Task<ErrorResponce> AddEditScores(AddEditScoresView View)
        {
            if (View.Reason == null || View.TeacherId == 0 || View.StudentId == 0 || View.ScoreId == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            await unitOfWork.Repository<StudentScoreEdit, long>().AddAsync(new StudentScoreEdit()
            {
                TeacherId = View.TeacherId,
                StudentScoreDataId = View.ScoreId,
                StudentId = View.StudentId,
                Reason = View.Reason,
                IsEdited = false
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
                return new ErrorResponce(200, "تم إضافة الدرجات بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> EditStudentScore(EditStudentScoreView view)
        {
            if (view.StudentScoreDataId == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var Score = await unitOfWork.Repository<StudentScoreData, long>().GetByIdAsync(new StudentScoreDataSpecification(view.StudentScoreDataId));
            Score.Behavior = view.Behavior;
            Score.Homework = view.Homework;
            Score.Review = view.Review;
            Score.Activity = view.Activity;
            Score.Missions = view.Missions;
            Score.Oral = view.Oral;
            Score.Classwork = view.Classwork;
            Score.IsPreAbsent = false;
            unitOfWork.Repository<StudentScoreData, long>().Update(Score);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
                return new ErrorResponce(200, "تم التعديل بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<ScoreForPreAbsentData>> ScoreForPreAbsent(ScoreForPreAbsentView view)
        {
            var Data = new List<ScoreForPreAbsentData>();
            var TCs = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                TeacherId = view.TeacherId
            }));
            TCs = TCs.Where(p => p.Class.SchoolId == view.SchoolId);
            foreach (var Tc in TCs)
            {
                var Scores = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
                {
                    ClassId = Tc.ClassId,
                    IsPreAbsent = true,
                    IsSaved = true,
                    SubjectId = Tc.SubjectId,
                }));
                int l = HelperFn.GetLevel(Tc.Class.FullId);
                foreach (var Score in Scores)
                {
                    Data.Add(new ScoreForPreAbsentData()
                    {
                        Activity = Score.Activity,
                        Behavior = Score.Behavior,
                        Classwork = Score.Classwork,
                        Homework = Score.Homework,
                        Missions = Score.Missions,
                        Oral = Score.Oral,
                        Review = Score.Review,
                        ScoreId = Score.Id,
                        StudentName = Score.Student.Name,
                        SubjectName = Score.Subject.Name,
                        ClassFullId = Score.Class.FullId,
                        WeekName = Score.Week.Name,
                        Weekindex = Score.Week.Index,
                        level = l
                    });
                }
            }
            return Data.OrderBy(p => p.SubjectName).ThenBy(p => p.Weekindex).ThenBy(p => p.ClassFullId).ThenBy(p => p.StudentName).ToList();
        }
    }
}