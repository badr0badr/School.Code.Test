﻿using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Other;
using Application.Core.Views.Student;
using Application.Core.Views.Teacher;
using Application.Repository.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Application.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public StudentService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        private bool CeratePdf(GetAllStudentStaticsSchoolData data, string FileName, string header, string SchoolName)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
            var IMFolderName = Path.Combine("wwwroot", "Resources", "Image");
            var IMFilePath = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName, "Mlogo.png");
            var imgbyts = File.ReadAllBytes(IMFilePath);
            IContainer bordercell(IContainer container)
            {
                return container.Border(1)
                                .BorderColor(Colors.Black)
                                .PaddingHorizontal(10)
                                .PaddingVertical(5);
            }
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(4, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(12));
                    page.Header()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn(8);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignBottom().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                });
                                table.Cell().AlignRight().Text("مديرية التربية والتعليم بالمنوفية").FontSize(10);
                                table.Cell().AlignRight().Text("إدارة سرس الليان التعليمية").FontSize(10);
                                table.Cell().AlignRight().Text($"مدرسة {SchoolName}").FontSize(10);
                            });
                            table.Cell().AlignBottom().AlignRight().Text(header).FontSize(16);
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        col.Item().Text("إجمالي المدرسة").AlignCenter();
                        col.Item().Text($"إجمالي الطلاب {data.TotalStudents}").AlignCenter();
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("أنثى").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("مسلم").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("مسيحي").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("مستجد").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("باقي").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("مجمد").Bold();
                            });
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalMale.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalFemale.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalMuslim.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalCruchin.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalNew.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalStay.ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.TotalFreze.ToString());
                        });
                        foreach (var Level in data.Levels)
                        {
                            if (data.Levels.Count() > 1)
                            {
                                col.Item().Text($"إجمالي المرحلة {Level.LevelName}").AlignCenter();
                                col.Item().Text($"إجمالي الطلاب {Level.TotalStudents}").AlignCenter();
                                col.Item().Table(table =>
                                {

                                    table.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                    });
                                    table.Header(h =>
                                    {
                                        h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("أنثى").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسلم").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسيحي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مستجد").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("باقي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مجمد").Bold();
                                    });
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalMale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalFemale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalMuslim.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalCruchin.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalNew.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalStay.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Level.TotalFreze.ToString());
                                });
                            }
                            foreach (var Grade in Level.Grades)
                            {
                                col.Item().Text($"إجمالي الصف {Grade.GradeName}").AlignCenter();
                                col.Item().Text($"إجمالي الطلاب {Grade.TotalStudents}").AlignCenter();
                                col.Item().Table(table =>
                                {

                                    table.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                    });
                                    table.Header(h =>
                                    {
                                        h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("أنثى").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسلم").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسيحي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مستجد").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("باقي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مجمد").Bold();
                                    });
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalMale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalFemale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalMuslim.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalCruchin.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalNew.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalStay.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalFreze.ToString());
                                });
                            }
                        }
                        col.Item().PageBreak();
                        foreach (var Level in data.Levels)
                        {
                            foreach (var Grade in Level.Grades)
                            {
                                col.Item().Text($"إجمالي الصف {Grade.GradeName}").AlignCenter();
                                col.Item().Text($"إجمالي الطلاب {Grade.TotalStudents}").AlignCenter();
                                col.Item().Table(table =>
                                {

                                    table.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                    });
                                    table.Header(h =>
                                    {
                                        h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("أنثى").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسلم").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسيحي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مستجد").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("باقي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مجمد").Bold();
                                    });
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalMale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalFemale.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalMuslim.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalCruchin.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalNew.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalStay.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(Grade.TotalFreze.ToString());
                                });
                                col.Item().Text("");
                                col.Item().Table(table =>
                                {

                                    table.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                        c.RelativeColumn(1);
                                    });
                                    table.Header(h =>
                                    {
                                        h.Cell().Element(bordercell).AlignCenter().Text("الفصل").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("إجمالي الطلاب").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("ذكر").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("أنثى").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسلم").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مسيحي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مستجد").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("باقي").Bold();
                                        h.Cell().Element(bordercell).AlignCenter().Text("مجمد").Bold();
                                    });
                                    foreach (var Class in Grade.Classes)
                                    {
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.ClassName);
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalStudents.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalMale.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalFemale.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalMuslim.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalCruchin.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalNew.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalStay.ToString());
                                        table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalFreze.ToString());
                                    }
                                });
                                col.Item().PageBreak();
                            }
                        }
                    });
                    page.Footer()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("شئون طلاب ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                            table.Cell().ColumnSpan(2).AlignCenter().Text(" ");
                            table.Cell().ColumnSpan(2).AlignCenter().Text(" ");
                        });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        public async Task<ErrorResponce> AddStudent(AddStudentView Addstudent)
        {
            if (Addstudent is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var Class = await context.Classes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == Addstudent.ClassId);
            if (Class == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            await unitOfWork.Repository<Student, long>().AddAsync(new Student()
            {
                Name = Addstudent.Name,
                ClassId = Addstudent.ClassId,
                Gender = Addstudent.Gender,
                Religion = Addstudent.Religion,
                Status = Addstudent.Status,
                MargeType = Addstudent.MargeType ?? "طبيعي",
                IsDeleted = false,
                IsHome= false,
                SchoolId= Class.SchoolId,
                ClassName=Class.Name
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddStudentRange(List<AddStudentView> Addstudent)
        {
            if (Addstudent is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            foreach (var item in Addstudent)
            {
                var Class = await context.Classes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == item.ClassId);
                if (Class == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
                await unitOfWork.Repository<Student, long>().AddAsync(new Student()
                {
                    Name = item.Name,
                    ClassId = item.ClassId,
                    Gender = item.Gender,
                    Religion = item.Religion,
                    Status = item.Status,
                    MargeType = item.MargeType ?? "طبيعي",
                    IsDeleted = false,
                    IsHome = false,
                    SchoolId = Class.SchoolId,
                    ClassName = Class.Name
                });
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<ClassStudentsView>> GetClassStudents(long ClassId)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = ClassId
            }));
            return Student.Select(p => new ClassStudentsView()
            {
                Id = p.Id,
                Gender = p.Gender,
                Name = p.Name,
                Religion = p.Religion,
                Status = p.Status,
                MargeType = p.MargeType,
            }).ToList();
        }
        public async Task<ErrorResponce> EditStudent(EditStudentView student)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(student.Id));
            if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
            Student.Name = student.Name ?? Student.Name;
            Student.Gender = student.Gender ?? Student.Gender;
            Student.Religion = student.Religion ?? Student.Religion;
            Student.Status = student.Status ?? Student.Status;
            Student.MargeType = student.MargeType ?? Student.MargeType;
            unitOfWork.Repository<Student, long>().Update(Student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تعديل بيانات الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> EditStudentRange(List<EditStudentView> view)
        {
            foreach (var student in view)
            {
                var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(student.Id));
                if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
                Student.Name = student.Name ?? Student.Name;
                Student.Gender = student.Gender ?? Student.Gender;
                Student.Religion = student.Religion ?? Student.Religion;
                Student.Status = student.Status ?? Student.Status;
                Student.MargeType = student.MargeType ?? Student.MargeType;
                unitOfWork.Repository<Student, long>().Update(Student);
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تعديل بيانات الطلاب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> EditStudentClass(EditStudentView view)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(view.Id));
            if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
            if(view.ClassId == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            if (view.ClassId == Student.ClassId) return new ErrorResponce(400, "الطالب موجود في هذا الفصل يجب تغيير الفصل");
            foreach (var item in Student.Scores)
            {
                item.ClassId = view.ClassId.Value;
                unitOfWork.Repository<StudentScoreData, long>().Update(item);
            }
            foreach (var item in Student.Exams)
            {
                item.ClassId = view.ClassId.Value;
                unitOfWork.Repository<StudentExamScoreData, long>().Update(item);
            }
            foreach (var item in Student.Absents)
            {
                item.ClassId = view.ClassId.Value;
                unitOfWork.Repository<StudentAbsentData, long>().Update(item);
            }
            Student.ClassId = view.ClassId ?? Student.ClassId;
            unitOfWork.Repository<Student, long>().Update(Student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تعديل بيانات الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> EditStudentAbsentDays(EditStudentAbsentDaysView view)
        {
            if (view is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var StudentAbsents = await unitOfWork.Repository<StudentAbsentCases, long>().GetAllAsync(new StudentAbsentCasesSpecification(new StudentAbsentCasesParams()
            {
                StudentId = view.StudentId
            }));
            var Studentdata = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                StudentId = view.StudentId
            }));
            var StudentAbsent = StudentAbsents.FirstOrDefault(p => (p.StartDate <= view.StartDate && p.EndDate >= view.StartDate) ||
                                                                   (p.StartDate <= view.EndDate && p.EndDate >= view.EndDate));
            if (StudentAbsent != null) return new ErrorResponce(400, "الطالب لدية عذر لهذا اليوم بالفعل");
            var AllWeeks = await unitOfWork.Repository<Week, long>().GetAllAsync(new WeekSpecification());
            var Weeks = AllWeeks.Where(w => (view.StartDate >= w.StartDate && view.StartDate <= w.EndDate) ||
                                            (view.EndDate >= w.StartDate && view.EndDate <= w.EndDate) ||
                                            (view.StartDate <= w.StartDate && view.StartDate <= w.EndDate && view.EndDate >= w.StartDate && view.EndDate >= w.EndDate)).ToList();
            foreach (var week in Weeks)
            {
                foreach (var data in Studentdata)
                {
                    if (Studentdata.FirstOrDefault(p => p.Id == data.Id && p.WeekId == week.Id) == null) continue;
                    Studentdata.First(p => p.Id == data.Id && p.WeekId == week.Id).IsPreAbsent = false;
                }
            }
            unitOfWork.Repository<StudentScoreData, long>().UpdateRange(Studentdata);
            await unitOfWork.Repository<StudentAbsentCases, long>().AddAsync(new StudentAbsentCases()
            {
                StudentId = view.StudentId,
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                Reason = view.Reason
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافة الاجازة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteStudent(long StudentId)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(StudentId));
            if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
            unitOfWork.Repository<Student, long>().Delete(Student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> RestoreStudent(long StudentId)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(StudentId));
            if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
            Student.IsDeleted = false;
            unitOfWork.Repository<Student, long>().Update(Student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم استعادة الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> SoftDeleteStudent(long StudentId)
        {
            var Student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(StudentId));
            if (Student is null) return new ErrorResponce(400, "هذا الطالب غير موجود");
            Student.IsDeleted = true;
            unitOfWork.Repository<Student, long>().Update(Student);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف الطالب");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<IdNumberNameView>> GetDeletedStudents()
        {
            var Students = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(new StudentParams()
            {
                IsDeleted = true
            }));
            return Students.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<ErrorResponce> DeleteClass(long ClassId)
        {
            if (ClassId == null) return new ErrorResponce(400, "ادخل الفصل");
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdAsync(new ClassesSpecification(ClassId));
            if (Class is null) return new ErrorResponce(400, "هذا الفصل غير موجود");
            if (Class.Students.Count() > 0) return new ErrorResponce(400, "لا يمكن حذف الفصل يجب نقل الطلاب بداخله اولا");
            unitOfWork.Repository<Classes, long>().Delete(Class);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الحذف");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> SendStudentToAnotherSchool(SendStudentToAnotherSchoolView view)
        {
            await unitOfWork.Repository<SendStudentToSchool, long>().AddAsync(new SendStudentToSchool()
            {
                StudentId = view.StudentId,
                SchoolSenderId = view.OldSchool,
                SchoolReceiverId = view.NewSchool,
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تقديم طلب النقل");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<GetTransformationsView>> GetStudentTransformationsReceiver(long SchoolId)
        {
            var transformation = await unitOfWork.Repository<SendStudentToSchool, long>().GetAllAsync(new SendStudentToSchoolSpecification(new SendStudentToSchoolParams()
            {
                SchoolReceiverId = SchoolId
            }));
            return transformation.Select(p => new GetTransformationsView()
            {
                Id = p.Id,
                SchoolName = p.SchoolSender.Name,
                StudentName = p.Student.Name,
                CanBeSend = p.CanBeSend
            }).ToList();
        }
        public async Task<List<GetTransformationsView>> GetStudentTransformationsSender(long SchoolId)
        {
            var transformation = await unitOfWork.Repository<SendStudentToSchool, long>().GetAllAsync(new SendStudentToSchoolSpecification(new SendStudentToSchoolParams()
            {
                SchoolSenderId = SchoolId
            }));
            return transformation.Select(p => new GetTransformationsView()
            {
                Id = p.Id,
                SchoolName = p.SchoolReceiver.Name,
                StudentName = p.Student.Name,
                CanBeSend = p.CanBeSend
            }).ToList();
        }
        public async Task<ErrorResponce> AcceptStudentTransformation(AcceptStudentTransformationView view)
        {
            var transformation = await unitOfWork.Repository<SendStudentToSchool, long>().GetByIdAsync(new SendStudentToSchoolSpecification(view.Id));
            if (transformation is null) return new ErrorResponce(400, "طلب النقل غير موجود");
            if (transformation.CanBeSend == false) return new ErrorResponce(400, "لا يمكن نقل الطالب إلا بموافقة من الإدارة التعليمية");
            var student = await unitOfWork.Repository<Student, long>().GetByIdAsync(new StudentSpecification(transformation.StudentId));
            if (student is null) return new ErrorResponce(400, "الطالب غير موجود");
            foreach (var item in student.Scores)
            {
                item.ClassId = view.ClassId;
                unitOfWork.Repository<StudentScoreData, long>().Update(item);
            }
            foreach (var item in student.Exams)
            {
                item.ClassId = view.ClassId;
                unitOfWork.Repository<StudentExamScoreData, long>().Update(item);
            }
            foreach (var item in student.Absents)
            {
                item.ClassId = view.ClassId;
                unitOfWork.Repository<StudentAbsentData, long>().Update(item);
            }
            student.ClassId = view.ClassId;
            unitOfWork.Repository<Student, long>().Update(student);
            unitOfWork.Repository<SendStudentToSchool, long>().Delete(transformation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم نقل الطالب إلى المدرسة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<GetAllStudentStaticsSchoolData> GetAllStudentStatics(long SchoolId)
        {
            var Data = new GetAllStudentStaticsSchoolData();
            Data.ReportName = $"SchoolStudentStatics {SchoolId}";
            var AllClasses = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }));
            AllClasses = AllClasses.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList();
            var Levels = AllClasses.OrderBy(p => p.ClassType).DistinctBy(p => p.Grade).Select(p => p.Grade).ToList();
            foreach (var level in Levels)
            {
                var DataLevel = new GetAllStudentStaticsLevelsData();
                DataLevel.LevelName = level;
                var Grades = AllClasses.Where(p => p.Grade == level).DistinctBy(p => p.Name).Select(p => p.Name).ToList();
                foreach (var Grade in Grades)
                {
                    var DataGrade = new GetAllStudentStaticsGradesData();
                    DataGrade.GradeName = Grade;
                    var classes = AllClasses.Where(p => p.Name == Grade).ToList();
                    foreach (var Class in classes)
                    {
                        var Students = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(new StudentParams()
                        {
                            ClassId = Class.Id,
                        }));
                        var DataClass = new GetAllStudentStaticsClassesData()
                        {
                            ClassName = Class.FullId,
                            ClassId = Class.Id,
                            TotalMuslim = Students.Where(p => p.Religion == "مسلم").Count(),
                            TotalCruchin = Students.Where(p => p.Religion == "مسيحي").Count(),
                            TotalMale = Students.Where(p => p.Gender == "ذكر").Count(),
                            TotalFemale = Students.Where(p => p.Gender == "أنثى").Count(),
                            TotalNew = Students.Where(p => p.Status == "مستجد").Count(),
                            TotalStay = Students.Where(p => p.Status == "باقي").Count(),
                            TotalFreze = Students.Where(p => p.Status == "مجمد").Count(),
                            TotalStudents = Students.Count()
                        };
                        DataGrade.Classes.Add(DataClass);
                        DataGrade.TotalNew += DataClass.TotalNew;
                        DataGrade.TotalFreze += DataClass.TotalFreze;
                        DataGrade.TotalFemale += DataClass.TotalFemale;
                        DataGrade.TotalMale += DataClass.TotalMale;
                        DataGrade.TotalMuslim += DataClass.TotalMuslim;
                        DataGrade.TotalCruchin += DataClass.TotalCruchin;
                        DataGrade.TotalStay += DataClass.TotalStay;
                        DataGrade.TotalStudents += DataClass.TotalStudents;
                    }
                    DataLevel.Grades.Add(DataGrade);
                    DataLevel.TotalNew += DataGrade.TotalNew;
                    DataLevel.TotalFreze += DataGrade.TotalFreze;
                    DataLevel.TotalFemale += DataGrade.TotalFemale;
                    DataLevel.TotalMale += DataGrade.TotalMale;
                    DataLevel.TotalMuslim += DataGrade.TotalMuslim;
                    DataLevel.TotalCruchin += DataGrade.TotalCruchin;
                    DataLevel.TotalStay += DataGrade.TotalStay;
                    DataLevel.TotalStudents += DataGrade.TotalStudents;
                }
                Data.Levels.Add(DataLevel);
                Data.TotalNew += DataLevel.TotalNew;
                Data.TotalFreze += DataLevel.TotalFreze;
                Data.TotalFemale += DataLevel.TotalFemale;
                Data.TotalMale += DataLevel.TotalMale;
                Data.TotalMuslim += DataLevel.TotalMuslim;
                Data.TotalCruchin += DataLevel.TotalCruchin;
                Data.TotalStay += DataLevel.TotalStay;
                Data.TotalStudents += DataLevel.TotalStudents;
            }
            var Created = CeratePdf(Data, Data.ReportName, $"", AllClasses.First().School.Name);
            if (Created)
                return Data;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<List<UploadedStudentNames>> UploadStudentNames(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new Exception("يرجي رفع الملف");
            var folderName = Path.Combine("Resources", "Data", "Upload");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var fileName = file.FileName;
            var Ext = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            if (Ext.ToLower() != "txt") throw new Exception("صيغة هذا الملف غير مدعوم يجب ان يكون في امتداد .txt");
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var Reportsdata = File.ReadAllLines(fullPath);
            if (Reportsdata is not null && Reportsdata.Count() > 0)
            {
                var data = new List<UploadedStudentNames>();
                int i = 1;
                foreach (var report in Reportsdata)
                {
                    if (string.IsNullOrWhiteSpace(report) || string.IsNullOrEmpty(report)) continue;
                    else if (report.Length < 10) data.Add(new UploadedStudentNames() { Id = i, Name = report, IsValid = false });
                    else if (report.Length >= 10) data.Add(new UploadedStudentNames() { Id = i, Name = report, IsValid = true });
                    else data.Add(new UploadedStudentNames() { Id = i, Name = report, IsValid = false });
                    i++;
                }
                File.Delete(fullPath);
                return data;
            }
            File.Delete(fullPath);
            throw new Exception("خطأ في الرفع يرجي الرفع مرة اخري");
        }
        public async Task<ErrorResponce> UploadStudentCodes(IFormFile file)
        {
            int index = 0;
            try
            {
                if (file == null || file.Length == 0) return new ErrorResponce(400, "No file uploaded");
                var folderName = Path.Combine("Resources", "Data", "Upload");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var fileName = file.FileName;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var Reportsdata = File.ReadAllText(fullPath, System.Text.Encoding.UTF8);
                var Reports = JsonSerializer.Deserialize<List<StudentCodeUpdate>>(Reportsdata);
                var existingStudents = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(false));
                if (Reports is not null && Reports.Count() > 0)
                {
                    foreach (var report in Reports)
                    {
                        index++;
                        var existingStudent = existingStudents.FirstOrDefault(s => s.Id == report.Id);
                        if (existingStudent == null) continue;
                        existingStudent.Code = report.Code;
                        unitOfWork.Repository<Student, long>().Update(existingStudent);
                    }
                }
                File.Delete(fullPath);
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "Added");
                return new ErrorResponce(400, "Error");
            }
            catch (Exception ex)
            {
                return new ErrorResponce(400, ex.Message + " at : " + index);
            }
        }
    }
}