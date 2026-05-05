﻿using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
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
        #region Applied Exam
        //إعدادي فقط
        public async Task<List<StudentsForAppliedExamData>> GetStudentsForAppliedExam(StudentsForAppliedExamView view)
        {
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
        public async Task<ErrorResponce> SaveStudentsForAppliedExam(SaveStudentsForAppliedExamView view)
        {
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
        #endregion

        #region Qr Code
        public async Task<ErrorResponce> CheakCode(string Token, int Code)
        {
            var teacherId = tokenService.GetTeacherFromToken(Token);
            if (teacherId == null) return new ErrorResponce(400, "غير مصرح");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(teacherId.Id));
            if (teacher == null) return new ErrorResponce(400, "غير مصرح");
            if (teacher.CanAccesControl == false) return new ErrorResponce(400, "غير مصرح");
            var studentCode = await unitOfWork.Repository<ExamCodes, long>().GetByIdAsync(new ExamCodesSpecification(new ExamCodesParams()
            {
                Code = Code
            }));
            if (studentCode == null) return new ErrorResponce(404, "تلاعب في QR Code");
            return new ErrorResponce(200, $"Code : {Code}");
        }
        public async Task<ErrorResponce> AddExamResult(string Token, AddExamResultView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            var teacherId = tokenService.GetTeacherFromToken(Token);
            if (teacherId == null) return new ErrorResponce(400, "غير مصرح");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(teacherId.Id));
            if (teacher == null) return new ErrorResponce(400, "غير مصرح");
            if (teacher.CanAccesControl == false) return new ErrorResponce(400, "غير مصرح");
            if (teacher.ControlType != "Adder") return new ErrorResponce(400, "غير مصرح");
            var studentCode = await unitOfWork.Repository<ExamCodes, long>().GetByIdAsync(new ExamCodesSpecification(new ExamCodesParams()
            {
                Code = view.Code
            }));
            if (studentCode == null) return new ErrorResponce(404, "تلاعب في QR Code");
            studentCode.Result = view.Score;
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ViewStudentData(string Token, int Code)
        {
            var teacherId = tokenService.GetTeacherFromToken(Token);
            if (teacherId == null) return new ErrorResponce(400, "غير مصرح");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(teacherId.Id));
            if (teacher == null) return new ErrorResponce(400, "غير مصرح");
            if (teacher.CanAccesControl == false) return new ErrorResponce(400, "غير مصرح");
            if (teacher.ControlType != "ControlManager") return new ErrorResponce(400, "غير مصرح");
            var studentCode = await unitOfWork.Repository<ExamCodes, long>().GetByIdAsync(new ExamCodesSpecification(new ExamCodesParams()
            {
                Code = Code
            }));
            if (studentCode == null) return new ErrorResponce(404, "تلاعب في QR Code");
            return new ErrorResponce(200, $"Code : {Code}    اسم الطالب : {studentCode.Student.Name}");
        }
        #endregion

        #region Viewer Work
        public async Task<List<string>> GetAllFileNamesInFolder()
        {
            var FolderName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Resources", "ControlPDFs");
            if (!Directory.Exists(FolderName))
            {
                throw new DirectoryNotFoundException($"The folder path '{FolderName}' does not exist.");
            }
            var fileNames = Directory.GetFiles(FolderName).Select(Path.GetFileName).ToList();
            return await Task.FromResult(fileNames);
        }
        #endregion
    }
}