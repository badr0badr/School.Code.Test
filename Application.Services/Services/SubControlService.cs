﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class SubControlService : ISubControlService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;
        private readonly AppDbContext context;

        public SubControlService(IUnitOfWork _unitOfWork, ITokenService _tokenService, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            tokenService = _tokenService;
            context = _context;
        }
        //Manger   Adder   Viewer  ControlManager
        private async Task<bool> CanAccessControl(string Token, string? Type = null, string? Type2 = null)
        {
            var teacherId = tokenService.GetTeacherFromToken(Token);
            if (teacherId == null) throw new Exception("غير مصرح");
            var teacher = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.Id == teacherId.Id);
            if (teacher == null) throw new Exception("غير مصرح");
            if (teacher.CanAccesControl == false) throw new Exception("غير مصرح");
            if (Type != null && teacher.ControlType == Type) return true;
            else if (Type2 != null && teacher.ControlType == Type2) return true;
            else if(Type == null && Type2 == null) return true;
            throw new Exception("غير مصرح");
        }
        #region Adder Work
        #region Exams
        //إعدادي فقط
        public async Task<List<StudentsForAppliedExamData>> GetStudentsForAppliedExam(string Token, StudentsForAppliedExamView view)
        {
            await CanAccessControl(Token, "Adder");
            var Students = await context.Student.AsNoTracking().Where(p => p.ClassId == view.ClassId).OrderBy(p => p.Name).ToListAsync();
            if (await context.FinalScoresData.AnyAsync(p => p.ClassId == view.ClassId &&
                                                            p.SubjectId == view.SubjectId &&
                                                            p.Term == 2 && p.IsAppliedSaved)) throw new Exception("تم رفع الدرجات مسبقا");
            var Scores = await context.FinalScoresData.AsNoTracking()
                                                      .Where(p => p.ClassId == view.ClassId &&
                                                                  p.SubjectId == view.SubjectId &&
                                                                  p.Term == 2)
                                                      .ToListAsync();
            var data = new List<StudentsForAppliedExamData>();
            foreach (var student in Students)
            {
                var sc = Scores.FirstOrDefault(p => p.StudentId == student.Id);
                data.Add(new StudentsForAppliedExamData()
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    Score = sc == null ? 0 : sc.AppliedExam
                });
            }
            return data;
        }
        public async Task<ErrorResponce> SaveStudentsForAppliedExam(string Token, SaveStudentsForAppliedExamView view)
        {
            await CanAccessControl(Token, "Adder");
            var Scores = await context.FinalScoresData.Where(p => p.ClassId == view.ClassId &&
                                                                  p.SubjectId == view.SubjectId &&
                                                                  p.Term == 2)
                                                      .ToListAsync();
            foreach (var student in view.Students)
            {
                var ES = Scores.FirstOrDefault(p => p.StudentId == student.StudentId);
                if (ES == null)
                {
                    await unitOfWork.Repository<FinalScoresData, long>().AddAsync(new FinalScoresData()
                    {
                        AppliedExam = student.Score,
                        IsAppliedSaved = view.IsSave,
                        Behavior = 0,
                        EducationalYear = "2025/2026",
                        ClassId = view.ClassId,
                        Exam = 0,
                        FinalExam = 0,
                        Homework = 0,
                        IsPassed = false,
                        Review = 0,
                        StudentId = student.StudentId,
                        SubjectId = view.SubjectId,
                        Term = 2
                    });
                }
                else
                {
                    ES.AppliedExam = student.Score;
                    ES.IsAppliedSaved = view.IsSave;
                    unitOfWork.Repository<FinalScoresData, long>().Update(ES);
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<List<StudentExamAbsentData>> GetExamAbsent(string Token, GetExamAbsentView view)
        {
            await CanAccessControl(Token, "Adder");
            var StudentsExamCodes = await context.ExamCodes.AsNoTracking()
                                                  .Include(p => p.Student)
                                                  .Where(p => p.SubjectId == view.SubjectId &&
                                                             (p.Student.ClassName == view.ClassName || p.Student.Class.Name == view.ClassName) &&
                                                              p.Student.HallNumber == view.HallNumber)
                                                  .OrderBy(p => p.Student.PlaceInHall)
                                                  .ToListAsync();
            if (StudentsExamCodes.Count() == 0) throw new Exception("خطأ في رقم اللجنة");
            if (StudentsExamCodes.Any(p => p.IsExist)) throw new Exception("تم رصد الغياب مسبقأ");
            return StudentsExamCodes.Select(p => new StudentExamAbsentData()
            {
                Id = p.Id,
                Name = p.Student.Name,
                IsExist = !p.IsExist,
            }).ToList();
        }
        public async Task<ErrorResponce> SaveExamAbsent(string Token, SaveExamAbsentView view)
        {
            await CanAccessControl(Token, "Adder");
            var StudentsExamCodes = await context.ExamCodes.AsNoTracking()
                                                  .Include(p => p.Student)
                                                  .Where(p => p.SubjectId == view.SubjectId &&
                                                             (p.Student.ClassName == view.ClassName || p.Student.Class.Name == view.ClassName) &&
                                                              p.Student.HallNumber == view.HallNumber)
                                                  .OrderBy(p => p.Student.PlaceInHall)
                                                  .ToListAsync();
            if (StudentsExamCodes.Count() == 0) throw new Exception("خطأ في رقم اللجنة");
            foreach (var student in view.Students)
            {
                var SC = StudentsExamCodes.FirstOrDefault(p => p.Id == student.Id);
                if (SC != null)
                {
                    SC.IsExist = student.IsExist;
                    unitOfWork.Repository<ExamCodes, long>().Update(SC);
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الإضافة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        #endregion

        #region Qr Code
        public async Task<ErrorResponce> CheakCode(string Token, long Code)
        {
            await CanAccessControl(Token);
            var student = await context.ExamCodes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Code);
            if (student == null) return new ErrorResponce(404, "تلاعب في QR Code");
            return new ErrorResponce(200, $"السري : {student.Code}");
        }
        public async Task<ErrorResponce> AddExamResult(string Token, AddExamResultView view)
        {
            await CanAccessControl(Token, "Adder", "ControlManager");
            var student = await context.ExamCodes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.Code);
            if (student == null) return new ErrorResponce(404, "تلاعب في QR Code");
            student.Result = view.Score;
            unitOfWork.Repository<ExamCodes, long>().Update(student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ViewStudentData(string Token, long Code)
        {
            await CanAccessControl(Token, "ControlManager");
            var student = await context.ExamCodes.AsNoTracking().Include(p => p.Student).Include(p => p.Subject).FirstOrDefaultAsync(p => p.Id == Code);
            if (student == null) return new ErrorResponce(404, "تلاعب في QR Code");
            return new ErrorResponce(200, $"Code : {student.Code} | اسم الطالب : {student.Student.Name} | مادة : {student.Subject.Name} | الدرجة : {student.Result}");
        }
        #endregion

        #endregion

        #region Viewer Work
        public async Task<List<string>> GetAllFileNamesInFolder(string Token)
        {
            await CanAccessControl(Token, "Viewer");
            var FolderName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Resources", "ControlPDFs");
            if (!Directory.Exists(FolderName))
            {
                throw new DirectoryNotFoundException($"The folder path '{FolderName}' does not exist.");
            }
            var fileNames = Directory.GetFiles(FolderName).Select(Path.GetFileName).ToList();
            return await Task.FromResult(fileNames);
        }
        public async Task<List<HallSammryData>> HallSummryDatas(string Token, long SchoolId)
        {
            await CanAccessControl(Token, "Viewer");
            var School = await context.School.AsNoTracking().FirstOrDefaultAsync(p => p.Id == SchoolId);
            if (School == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var CM = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == SchoolId && p.ControlType == "ControlManager");
            var SM = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == SchoolId && p.RoleId == "Manager");
            if (CM == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            if (SM == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var ClassNames = await context.Classes.AsNoTracking()
                                                .Where(p => p.SchoolId == SchoolId)
                                                .OrderBy(p => p.ClassType)
                                                .ThenBy(p => p.Class)
                                                .ThenBy(p => p.ClassNumber)
                                                .Select(p => p.Name)
                                                .Distinct()
                                                .ToListAsync();
            var Students = await context.Student.AsNoTracking()
                                              .Include(p => p.Class)
                                              .Where(p => p.Class.SchoolId == SchoolId)
                                              .Select(p => new
                                              {
                                                  p.Id,
                                                  p.HallNumber,
                                                  ClassName = p.Class.Name,
                                                  p.PlaceNumber
                                              })
                                              .ToListAsync();
            var MaxHall = await context.Student.AsNoTracking().Where(p => p.Class.SchoolId == SchoolId).MaxAsync(p => p.HallNumber);
            var Result = new List<HallSammryData>();
            foreach (var ClassName in ClassNames)
            {
                for (int i = 1; i <= MaxHall; i++)
                {
                    var sts = Students.Where(p => p.ClassName == ClassName && p.HallNumber == i).ToList();
                    if (sts.Count == 0) continue;
                    Result.Add(new HallSammryData()
                    {
                        HallNumber = i,
                        ClassName = ClassName,
                        CM = CM.Name,
                        SM = SM.Name,
                        SchoolName = School.Name,
                        From = sts.Min(p => p.PlaceNumber),
                        To = sts.Max(p => p.PlaceNumber),
                        StudentsCount = sts.Count()
                    });
                }
            }
            return Result;
        }
        public async Task<List<StudentTackit>> GetStudentTackits(string Token, long SchoolId)
        {
            await CanAccessControl(Token, "Viewer");
            var school = await context.School.AsNoTracking().FirstOrDefaultAsync(p => p.Id == SchoolId);
            var ClassNames = await context.Classes.AsNoTracking()
                                                .Where(p => p.SchoolId == SchoolId)
                                                .OrderBy(p => p.ClassType)
                                                .ThenBy(p => p.Class)
                                                .ThenBy(p => p.ClassNumber)
                                                .Select(p => p.Name)
                                                .Distinct()
                                                .ToListAsync();
            var Students = await context.Student.AsNoTracking()
                                              .Include(p => p.Class)
                                              .Where(p => p.Class.SchoolId == SchoolId)
                                              .Select(p => new
                                              {
                                                  p.Id,
                                                  p.Name,
                                                  p.HallNumber,
                                                  ClassName = p.Class.Name,
                                                  p.PlaceNumber
                                              })
                                              .ToListAsync();
            var Result = new List<StudentTackit>();
            foreach (var ClassName in ClassNames)
            {
                foreach (var student in Students.Where(p => p.ClassName == ClassName).OrderBy(p => p.PlaceNumber).ToList())
                {
                    Result.Add(new StudentTackit()
                    {
                        HallNumber = student.HallNumber,
                        PlaceNumber = student.PlaceNumber,
                        StudentName = student.Name,
                        ClassName = ClassName,
                        SchoolName = school.Name
                    });
                }
            }
            return Result;
        }
        public async Task<List<ClassSubjectsDashBord>> GetSubjectDashbord(string Token, long schoolId)
        {
            await CanAccessControl(Token, "Viewer");
            var Result = new List<ClassSubjectsDashBord>();

            return Result;
        }
        private string Leters(int number)
        {
            switch (number)
            {
                case 1:
                    return "أ";

                case 2:
                    return "ب";
                case 3:
                    return "ج";
                case 4:
                    return "د";
                case 5:
                    return "ه";
                case 6:
                    return "و";
                case 7:
                    return "ز";
                case 8:
                    return "ح";
                case 9:
                    return "ط";
                default:
                    return "ي";

            }
        }
        public async Task<HandlingExamPapersToRateData> HandlingExamPapersToRate(string Token, HandlingExamPapersToRateView view)
        {
            await CanAccessControl(Token, "Viewer");
            var School = await context.School.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.SchoolId);
            if (School == null) throw new Exception(" تاكد من ادخال جميع البيانات");
            var ControlManager = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == view.SchoolId && p.ControlType == "ControlManager");
            if (ControlManager == null) throw new Exception(" تاكد من ادخال جميع البيانات");
            var SchoolManager = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == view.SchoolId && p.RoleId == "Manager");
            if (SchoolManager == null) throw new Exception(" تاكد من ادخال جميع البيانات");
            var SubjectName = await context.Subject.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.SubjectId);
            if (SubjectName == null) throw new Exception(" تاكد من ادخال جميع البيانات");
            var SubjectManager = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.RoleId == "TeacherSuper" && p.MainSubjectId == view.SubjectId);
            if (SubjectManager == null) throw new Exception(" تاكد من ادخال جميع البيانات");
            var ModelsData = new List<IdNumberNameView>();
            var Models = await context.Student.MaxAsync(p => p.ExamTemplte);
            var MargeTypes = await context.Student.Where(p => p.MargeType != "طبيعي" && p.MargeType != "بصرية").Select(p => p.MargeType).Distinct().ToListAsync();
                var result = new ExamTempltesModelData();
            for (sbyte i = 1; i <= Models; i++)
            {
                var AbsentMcount = await context.ExamCodes.CountAsync(p => (p.Student.Class.Name == view.ClassName || p.Student.ClassName == view.ClassName) &&
                                                                   p.SchoolId == view.SchoolId &&
                                                                   p.Student.MargeType == "طبيعي" &&
                                                                   p.Student.ExamTemplte == i &&
                                                                   p.IsExist);
                var AbsentVcount = await context.ExamCodes.CountAsync(p => (p.Student.Class.Name == view.ClassName || p.Student.ClassName == view.ClassName) &&
                                                                   p.SchoolId == view.SchoolId &&
                                                                   p.Student.MargeType == "بصرية" &&
                                                                   p.Student.ExamTemplte == i &&
                                                                   p.IsExist);
                if (AbsentMcount > 0)
                    ModelsData.Add(new IdNumberNameView()
                    {
                        Name = $"نموزج {Leters(i)}",
                        Id = AbsentMcount
                    });
                if (AbsentVcount > 0)
                    ModelsData.Add(new IdNumberNameView()
                    {
                        Name = $"دمج بصري نموزج {Leters(i)}",
                        Id = AbsentVcount
                    });
            }
            foreach (var ty in MargeTypes)
            {
                var Absentcount = await context.ExamCodes.CountAsync(p => (p.Student.Class.Name == view.ClassName || p.Student.ClassName == view.ClassName) &&
                                                                   p.SchoolId == view.SchoolId &&
                                                                   p.Student.MargeType == ty &&
                                                                   p.IsExist);
                if (Absentcount > 0)
                    ModelsData.Add(new IdNumberNameView()
                    {
                        Name = $"دمج {ty}",
                        Id = Absentcount
                    });
            }

            return new HandlingExamPapersToRateData()
            {
                SchoolManager = SchoolManager.Name,
                ControlManager = ControlManager.Name,
                ClassName = view.ClassName,
                Date = DateOnly.FromDateTime(DateTime.Now).ToString(),
                Day = DateHelper.DayNameAr(DateHelper.GetDayName(DateTime.Now)),
                SchoolName = School.Name,
                SubjectName = SubjectName.Name,
                SubjectManager = SubjectManager.Name,
                Models = ModelsData
            };
        }
        #endregion

        #region Manger Work

        #endregion

    }
}