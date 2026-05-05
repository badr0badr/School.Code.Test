﻿using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QRCoder;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.Base64QRCode;

namespace Application.Services.Services
{
    public class ControlService : IControlService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public ControlService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        //Manger   Adder   Viewer  Controller
        public async Task<List<IdStringNameView>> GetControlTypes()
        {
            var controlTypes = new List<IdStringNameView>
            {
                new IdStringNameView { Id = "ControlManager", Name = "مدير الكنترول" },
                new IdStringNameView { Id = "Viewer", Name = "مراقب" },
                new IdStringNameView { Id = "Adder", Name = "مضيف" }
            };
            return await Task.FromResult(controlTypes);
        }
        public async Task<ErrorResponce> AddToControl(AddToControlView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.TeacherId));
            if (teacher == null) return new ErrorResponce(404, "المعلم غير موجود");
            if (view.ControlType == "ControlManager")
            {
                var CM = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.ControlType == "ControlManager" &&
                                                                                       p.SchoolId == teacher.SchoolId);
                if (CM != null) return new ErrorResponce(404, "يوجد مدير الكنترول");
            }
            teacher.ControlType = view.ControlType;
            teacher.CanAccesControl = true;
            unitOfWork.Repository<Teacher, long>().Update(teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافة للكنترول");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<ControlTeachersData>> GetControlTeachers(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId,
                CanAccesControl = true
            }));
            var result = teachers.Select(t => new ControlTeachersData
            {
                Id = t.Id,
                Name = t.Name,
                ControlType = t.ControlType ?? ""
            }).ToList();
            return result;
        }
        public async Task<ErrorResponce> DeleteFromControl(long TeacherId)
        {
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(TeacherId));
            if (teacher == null) return new ErrorResponce(404, "المعلم غير موجود");
            teacher.ControlType = null;
            teacher.CanAccesControl = false;
            unitOfWork.Repository<Teacher, long>().Update(teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافة للكنترول");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddPlaceNumbers(long SchoolId)
        {
            var AllClassName = await context.Classes.AsNoTracking()
                                                 .Where(p => p.SchoolId == SchoolId)
                                                 .OrderBy(p => p.ClassType)
                                                 .ThenBy(p => p.Class)
                                                 .ThenBy(p => p.ClassNumber)
                                                 .ToListAsync();

            var ClassName = AllClassName.DistinctBy(p => p.Name)
                                        .Select(p => p.Name)
                                        .ToList();
            if (ClassName.Count == 0) return new ErrorResponce(404, "لا توجد فصول في المدرسة");
            foreach (var name in ClassName)
            {
                var students = await context.Student.Where(p => p.Class.SchoolId == SchoolId && p.Class.Name == name)
                                                    .OrderBy(p => p.Gender)
                                                    .ThenBy(p => EF.Functions.Collate(p.Name, "Arabic_100_CI_AS"))
                                                    .ToListAsync();
                if (students.Count == 0) continue;
                if (students.Any(p => p.PlaceNumber != 0)) return new ErrorResponce(404, "يوجد طلاب لديهم ارقام جلوس بالفعل");
                int placeNumber = 1;
                foreach (var student in students)
                {
                    student.PlaceNumber = placeNumber;
                    placeNumber++;
                }
                unitOfWork.Repository<Student, long>().UpdateRange(students);
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم إضافة ارقام الجلوس");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeletePlaceNumbers(long SchoolId)
        {
            var students = await context.Student.Where(p => p.Class.SchoolId == SchoolId && p.PlaceNumber != 0).ToListAsync();
            if (students.Count == 0) return new ErrorResponce(404, "لا توجد ارقام جلوس في المدرسة");
            foreach (var student in students)
            {
                student.PlaceNumber = 0;
            }
            unitOfWork.Repository<Student, long>().UpdateRange(students);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف ارقام الجلوس");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddSecriteCodes(long SchoolId)
        {
            var examCodes = await context.ExamCodes.Where(p => p.SchoolId == SchoolId).ToListAsync();
            if (examCodes.Count > 0) return new ErrorResponce(404, "يوجد أكواد سرية في المدرسة");
            var classesName = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }, false));
            if (classesName is null) throw new Exception("لا يوجد فصول");
            var ClassName = classesName.OrderBy(p => p.ClassType)
                                       .ThenBy(p => p.Class)
                                       .ThenBy(p => p.ClassNumber)
                                       .DistinctBy(p => p.Name)
                                       .Select(p => p.Name)
                                       .ToList();
            if (ClassName.Count == 0) return new ErrorResponce(404, "لا توجد فصول في المدرسة");
            var TC = await context.TeacherClasses.AsNoTracking()
                                                 .Include(p => p.Subject)
                                                 .Where(p => p.Class.SchoolId == SchoolId && p.Subject.Status != "Off")
                                                 .OrderBy(p => p.Subject.Index)
                                                 .ToListAsync();
            var subjects = TC.DistinctBy(p => p.SubjectId).Select(p => p.Subject).ToList();
            foreach (var name in ClassName)
            {
                var students = await context.Student.AsNoTracking()
                                                    .Where(p => p.Class.SchoolId == SchoolId && p.Class.Name == name)
                                                    .Select(p => p.Id)
                                                    .ToListAsync();
                if (students.Count == 0) continue;
                foreach (var subject in subjects)
                {
                    var Nstudents = students.OrderBy(p => Guid.NewGuid()).ToList();
                    for (int i = 0; i < students.Count(); i++)
                    {
                        await unitOfWork.Repository<ExamCodes, long>().AddAsync(new ExamCodes()
                        {
                            Code = i,
                            StudentId = Nstudents[i],
                            SchoolId = SchoolId,
                            SubjectId = subject.Id,
                            Result = 0
                        });
                    }
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم إضافة الأكواد السرية");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteSecriteCodes(long SchoolId)
        {
            var examCodes = await context.ExamCodes.Where(p => p.SchoolId == SchoolId).ToListAsync();
            if (examCodes.Count == 0) return new ErrorResponce(404, "لا توجد أكواد سرية في المدرسة");
            unitOfWork.Repository<ExamCodes, long>().DeleteRange(examCodes);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف الأكواد السرية");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> GetStudentCountInExamHall(StudentExamHallView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            if (view.ClassNames.Count == 0) return new ErrorResponce(404, "لا توجد فصول في المدرسة");
            if (view.ClassNames.Count > 2) return new ErrorResponce(404, "لا يمكن اختيار أكثر من فصلين لنفس لجان الإمتحانات");
            var ListCounter = new List<GenderSortMax>();
            foreach (var name in view.ClassNames)
            {
                ListCounter.Add(new GenderSortMax
                {
                    Counter = await context.Student.CountAsync(p => p.Class.SchoolId == view.SchoolId && p.Class.Name == name && p.Gender == "ذكر"),
                    Gender = "ذكر",
                    ClassName = name
                });
                ListCounter.Add(new GenderSortMax
                {
                    Counter = await context.Student.CountAsync(p => p.Class.SchoolId == view.SchoolId && p.Class.Name == name && p.Gender == "أنثى"),
                    Gender = "أنثى",
                    ClassName = name
                });
            }
            var MaxMale = ListCounter.Where(p => p.Gender == "ذكر").Max(p => p.Counter);
            var MaxFemale = ListCounter.Where(p => p.Gender == "أنثى").Max(p => p.Counter);
            var count = Math.Ceiling((MaxMale + MaxFemale) / view.Halls);
            if (count > 0) return new ErrorResponce(200, $"الفصل يستطيع استيعاب {count} طلاب من كل صف بعدد دسكات {MaxMale + MaxFemale} لكل اللجان");
            return new ErrorResponce(404, "لا توجد طلاب في الفصول");
        }
        public async Task<ErrorResponce> AddStudentInExamHall(StudentExamHallView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            if (view.ClassNames.Count == 0) return new ErrorResponce(404, "لا توجد فصول في المدرسة");
            if (view.ClassNames.Count > 2) return new ErrorResponce(404, "لا يمكن اختيار أكثر من فصلين لنفس لجان الإمتحانات");
            var Students = await context.Student.Where(p => p.Class.SchoolId == view.SchoolId && view.ClassNames.Contains(p.Class.Name))
                                                .Include(p => p.Class)
                                                .OrderBy(p => p.ClassId)
                                                .ThenBy(p => p.Name)
                                                .ToListAsync();
            if (Students.Count == 0) return new ErrorResponce(404, "لا توجد طلاب في الفصول");
            if (Students.Any(p => p.HallNumber != 0)) return new ErrorResponce(404, "يوجد طلاب في اللجان بالفعل");
            var ListCounter = new List<GenderSortMax>();
            foreach (var name in view.ClassNames)
            {
                ListCounter.Add(new GenderSortMax
                {
                    Counter = Students.Count(p => p.Class.Name == name && p.Gender == "ذكر"),
                    Gender = "ذكر",
                    ClassName = name,
                    Students = Students.Where(p => p.Class.Name == name && p.Gender == "ذكر").OrderBy(p => p.PlaceNumber).ToList()
                });
                ListCounter.Add(new GenderSortMax
                {
                    Counter = Students.Count(p => p.Class.Name == name && p.Gender == "أنثى"),
                    Gender = "أنثى",
                    ClassName = name,
                    Students = Students.Where(p => p.Class.Name == name && p.Gender == "أنثى").OrderBy(p => p.PlaceNumber).ToList()
                });
            }
            var MaxMale = ListCounter.OrderByDescending(p => p.Counter).First(p => p.Gender == "ذكر");
            var MinMale = ListCounter.OrderBy(p => p.Counter).First(p => p.Gender == "ذكر");
            var MaxFemale = ListCounter.OrderByDescending(p => p.Counter).First(p => p.Gender == "أنثى");
            var MinFemale = ListCounter.OrderBy(p => p.Counter).First(p => p.Gender == "أنثى");
            var HallCap1 = (int)Math.Floor((MaxMale.Counter + MaxFemale.Counter) / view.Halls);
            var HallCap2 = (int)Math.Round((((MaxMale.Counter + MaxFemale.Counter) / view.Halls) - HallCap1) * view.Halls, 0);
            var HallsCap = new int[(int)view.Halls];
            for (int i = 0; i < view.Halls; i++)
            {
                HallsCap[i] = HallCap1;
            }
            for (int i = 0; i < HallCap2; i++)
            {
                HallsCap[i]++;
            }


            int MaleIndex = 0;
            int FemaleIndex = 0;

            for (int i = 1; i <= view.Halls; i++)
            {
                for (int j = 1; j <= HallsCap[i - 1]; j++)
                {
                    if (FemaleIndex < MaxFemale.Counter)
                    {
                        MaxFemale.Students[FemaleIndex].HallNumber = i;
                        MaxFemale.Students[FemaleIndex].PlaceInHall = j;
                        if (view.ClassNames.Count > 1 && FemaleIndex < MinFemale.Students.Count)
                        {
                            MinFemale.Students[FemaleIndex].HallNumber = i;
                            MinFemale.Students[FemaleIndex].PlaceInHall = j;
                        }
                        FemaleIndex++;
                        continue;
                    }
                    if (MaleIndex >= MaxMale.Counter) break;
                    MaxMale.Students[MaleIndex].HallNumber = i;
                    MaxMale.Students[MaleIndex].PlaceInHall = j;
                    if (view.ClassNames.Count > 1 && MaleIndex < MinMale.Students.Count)
                    {
                        MinMale.Students[MaleIndex].HallNumber = i;
                        MinMale.Students[MaleIndex].PlaceInHall = j;
                    }
                    MaleIndex++;
                }
            }
            unitOfWork.Repository<Student, long>().UpdateRange(Students);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم توزيع الطلاب على اللجان");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteStudentFromExamHall(DeleteStudentExamView view)
        {
            var students = await context.Student.Where(p => p.Class.SchoolId == view.SchoolId).ToListAsync();
            if (students.Count == 0) return new ErrorResponce(404, "لا توجد طلاب في اللجان");
            foreach (var student in students)
            {
                student.HallNumber = 0;
                student.PlaceInHall = 0;
            }
            unitOfWork.Repository<Student, long>().UpdateRange(students);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف الطلاب من اللجان");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<StudentsPlaceNumbers> GetPlaceNumbers(GetPlaceNumbersView view)
        {
            var students = await context.Student.Where(p => p.Class.SchoolId == view.SchoolId && p.Class.Name == view.ClassName).ToListAsync();
            if (students.Count == 0) throw new Exception("لا توجد طلاب في الفصل");
            var school = await context.School.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.SchoolId);
            if (school == null) throw new Exception("المدرسة غير موجودة");
            var CM = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == view.SchoolId && p.ControlType == "ControlManager");
            if (CM == null) throw new Exception("يجب تعيين مدير كنترول");
            var SM = await context.Teacher.AsNoTracking().FirstAsync(p => p.SchoolId == view.SchoolId && p.RoleId == "Manager");
            var maxHall = students.Max(p => p.HallNumber);
            List<HallsPlaceData> Halls = new();
            for (int i = 1; i <= maxHall; i++)
            {
                var studentsInHall = students.Where(p => p.HallNumber == i).OrderBy(p => p.PlaceInHall).ToList();
                if (studentsInHall.Count == 0) continue;
                Halls.Add(new HallsPlaceData
                {
                    HallName = $"اللجنة {i}",
                    Students = studentsInHall.Select(s => new StudentsPlaceNumbersData
                    {
                        PlaceInHall = s.PlaceInHall,
                        StudentName = s.Name,
                        PlaceNumber = s.PlaceNumber
                    }).ToList()
                });
            }
            var data = new StudentsPlaceNumbers()
            {
                ReportName = $"Halls Sheet {students.First().Id}_{view.SchoolId}.pdf",
                Halls = Halls
            };
            var result = CeratePdf(data, school.Name, CM.Name, SM.Name);
            if (result) return data;
            throw new Exception("حدث خطأ اثناء انشاء الامتحانات");
        }
        private bool CeratePdf(StudentsPlaceNumbers data, string SchoolName, string CMName, string SMName)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, $"{data.ReportName}");
            var IMFolderName = Path.Combine("wwwroot", "Resources", "Image");
            var IMFilePath = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName, "Mlogo.png");
            var imgbyts = File.ReadAllBytes(IMFilePath);

            IContainer bordercell(IContainer container)
            {
                return container.Border(1)
                                .BorderColor(Colors.Black)
                                .PaddingHorizontal(3)
                                .PaddingVertical(3);
            }
            IContainer bordercell2(IContainer container)
            {
                return container.Border(4)
                                .BorderColor(Colors.Transparent)
                                .PaddingHorizontal(3)
                                .PaddingVertical(2);
            }
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(10));
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        for (int i = 0; i < data.Halls.Count; i++)
                        {
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                });

                                table.Cell().Element(bordercell2).AlignCenter().Table(TH =>
                                {
                                    TH.ColumnsDefinition(c =>
                                    {
                                        c.RelativeColumn(1);
                                    });
                                    TH.Cell().AlignCenter().Table(table2 =>
                                    {
                                        table2.ColumnsDefinition(c =>
                                        {
                                            c.RelativeColumn(3);
                                            c.RelativeColumn(1);
                                        });

                                        table2.Cell().AlignBottom().Table(table =>
                                        {
                                            table.ColumnsDefinition(c =>
                                            {
                                                c.RelativeColumn(1);
                                            });
                                            table.Cell().AlignRight().Text("مديرية التربية والتعليم بالمنوفية");
                                            table.Cell().AlignRight().Text("إدارة سرس الليان التعليمية");
                                            table.Cell().AlignRight().Text($"مدرسة {SchoolName}");
                                        });
                                        table2.Cell().Image(imgbyts).FitArea();
                                    });
                                    TH.Cell().AlignBottom().AlignCenter().Text("");
                                    TH.Cell().AlignBottom().AlignCenter().Text($"كشف {data.Halls[i].HallName}").FontSize(12);
                                    TH.Cell().AlignBottom().AlignCenter().Text("");
                                    TH.Cell().AlignCenter().Table(Tb =>
                                    {
                                        Tb.ColumnsDefinition(c =>
                                        {
                                            c.RelativeColumn(1);
                                            c.RelativeColumn(2);
                                            c.RelativeColumn(5);
                                        });
                                        Tb.Header(header =>
                                        {
                                            header.Cell().Element(bordercell).AlignCenter().Text("م");
                                            header.Cell().Element(bordercell).AlignCenter().Text("رقم الجلوس");
                                            header.Cell().Element(bordercell).AlignCenter().Text("اسم الطالب");
                                        });
                                        foreach (var student in data.Halls[i].Students)
                                        {
                                            Tb.Cell().Element(bordercell).AlignCenter().Text($"{student.PlaceInHall}");
                                            Tb.Cell().Element(bordercell).AlignCenter().Text($"{student.PlaceNumber}");
                                            Tb.Cell().Element(bordercell).AlignCenter().Text(student.StudentName);
                                        }
                                    });
                                    TH.Cell().AlignBottom().AlignCenter().Text("");
                                    TH.Cell().AlignCenter().Table(table =>
                                    {
                                        table.ColumnsDefinition(c =>
                                        {
                                            c.RelativeColumn(1);
                                            c.RelativeColumn(1);
                                        });
                                        table.Cell().AlignCenter().Text("مدير الكنترول");
                                        table.Cell().AlignCenter().Text("مدير المدرسة");
                                        table.Cell().ColumnSpan(2).AlignCenter().Text(" ");
                                        table.Cell().AlignCenter().Text($"{CMName}");
                                        table.Cell().AlignCenter().Text($"{SMName}");
                                    });
                                });
                                i++;
                                if (i < data.Halls.Count)
                                    table.Cell().Element(bordercell2).AlignCenter().Table(TH =>
                                    {
                                        TH.ColumnsDefinition(c =>
                                        {
                                            c.RelativeColumn(1);
                                        });
                                        TH.Cell().AlignCenter().Table(table2 =>
                                        {
                                            table2.ColumnsDefinition(c =>
                                            {
                                                c.RelativeColumn(3);
                                                c.RelativeColumn(1);
                                            });

                                            table2.Cell().AlignBottom().Table(table =>
                                            {
                                                table.ColumnsDefinition(c =>
                                                {
                                                    c.RelativeColumn(1);
                                                });
                                                table.Cell().AlignRight().Text("مديرية التربية والتعليم بالمنوفية");
                                                table.Cell().AlignRight().Text("إدارة سرس الليان التعليمية");
                                                table.Cell().AlignRight().Text($"مدرسة {SchoolName}");
                                            });
                                            table2.Cell().Image(imgbyts).FitArea();
                                        });
                                        TH.Cell().AlignBottom().AlignCenter().Text("");
                                        TH.Cell().AlignBottom().AlignCenter().Text($"كشف {data.Halls[i].HallName}").FontSize(12);
                                        TH.Cell().AlignBottom().AlignCenter().Text("");
                                        TH.Cell().AlignCenter().Table(Tb =>
                                        {
                                            Tb.ColumnsDefinition(c =>
                                            {
                                                c.RelativeColumn(1);
                                                c.RelativeColumn(2);
                                                c.RelativeColumn(5);
                                            });
                                            Tb.Header(header =>
                                            {
                                                header.Cell().Element(bordercell).AlignCenter().Text("م");
                                                header.Cell().Element(bordercell).AlignCenter().Text("رقم الجلوس");
                                                header.Cell().Element(bordercell).AlignCenter().Text("اسم الطالب");
                                            });
                                            foreach (var student in data.Halls[i].Students)
                                            {
                                                Tb.Cell().Element(bordercell).AlignCenter().Text($"{student.PlaceInHall}");
                                                Tb.Cell().Element(bordercell).AlignCenter().Text($"{student.PlaceNumber}");
                                                Tb.Cell().Element(bordercell).AlignCenter().Text(student.StudentName);
                                            }
                                        });
                                        TH.Cell().AlignBottom().AlignCenter().Text("");
                                        TH.Cell().AlignCenter().Table(table =>
                                        {
                                            table.ColumnsDefinition(c =>
                                            {
                                                c.RelativeColumn(1);
                                                c.RelativeColumn(1);
                                            });
                                            table.Cell().AlignCenter().Text("مدير الكنترول");
                                            table.Cell().AlignCenter().Text("مدير المدرسة");
                                            table.Cell().ColumnSpan(2).AlignCenter().Text(" ");
                                            table.Cell().AlignCenter().Text($"{CMName}");
                                            table.Cell().AlignCenter().Text($"{SMName}");
                                        });
                                    });
                            });
                            col.Item().PageBreak();
                        }
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        public async Task<ErrorResponce> PreparingExam(PreparingFinalExamView view)
        {
            var classes = await context.Classes.Where(p => p.SchoolId == view.SchoolId).AsNoTracking().ToListAsync();
            if (classes == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var ClassNames = classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).DistinctBy(p => p.Name).Select(p => p.Name).ToList();
            var Students = await context.Student.Where(p => p.Class.SchoolId == view.SchoolId).Include(p => p.Class).ToListAsync();
            if (Students == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            if (Students.Any(p => p.ExamTemplte != 0)) return new ErrorResponce(400, "تم توزيع نمازج الإمتحانات مسبقأ");
            foreach (var Class in ClassNames)
            {
                int inc = 1;
                var ClassStudents = Students.Where(p => p.Class.Name == Class).ToList();
                foreach (var student in ClassStudents)
                {
                    if (student.MargeType != "طبيعي" && student.MargeType != "بصرية")
                    {
                        student.ExamTemplte = 1;
                        unitOfWork.Repository<Student, long>().Update(student);
                        continue;
                    }
                    student.ExamTemplte = inc;
                    unitOfWork.Repository<Student, long>().Update(student);
                    inc++;
                    if (inc > view.Exams)
                        inc = 1;
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم توزيع نمازج الإمتحانات");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeletePreparingExam(long SchoolId)
        {
            var Students = await context.Student.Where(p => p.Class.SchoolId == SchoolId).ToListAsync();
            if (Students == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            foreach (var student in Students)
            {
                student.ExamTemplte = 1;
                unitOfWork.Repository<Student, long>().Update(student);
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف نمازج الإمتحانات");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<MerrorPdfData>> MerrorData(MerrorDataView view)
        {
            var school = await context.School.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.SchoolId);
            if (school == null) throw new Exception("تاكد من ادخال جميع البيانات");
            var Students = await context.Student.AsNoTracking()
                                                .Include(p => p.Class)
                                                .Where(p => p.Class.SchoolId == view.SchoolId && p.Class.Name == view.ClassName)
                                                .Select(p => new
                                                {
                                                    p.Id,
                                                    p.Name,
                                                    p.MargeType,
                                                    p.PlaceNumber,
                                                    p.ExamTemplte,
                                                    p.HallNumber,
                                                    index = p.MargeType == "طبيعي" || p.MargeType == "بصرية" ? 0 : 1
                                                })
                                                .ToListAsync();
            var subject = await context.Subject.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.SubjectId && p.Status != "Off");
            if (subject == null) throw new Exception("تاكد من ادخال جميع البيانات");

            var Secritecodes = await context.ExamCodes.AsNoTracking()
                                                    .Where(p => p.SchoolId == view.SchoolId && p.Subject.Status != "Off")
                                                    .Select(p => new
                                                    {
                                                        p.Id,
                                                        p.StudentId,
                                                        p.SubjectId,
                                                    })
                                                    .ToListAsync();
            Students = Students.OrderBy(p => p.index).ThenBy(p => p.ExamTemplte).ThenBy(p => p.PlaceNumber).ToList();
            var Data = new List<MerrorPdfData>();
            foreach (var student in Students)
            {
                var Secrite = Secritecodes.FirstOrDefault(p => p.StudentId == student.Id && p.SubjectId == subject.Id);
                if (Secrite == null)
                    throw new Exception($"الطالب : {student.Name} في الصف {view.ClassName} في المادة {subject.Name} ليس لدية رقم سري برجاء إعادة إنشاء الارقام السرية");
                Data.Add(new MerrorPdfData()
                {
                    ClassName = view.ClassName,
                    ExamTemplte = Leters(student.ExamTemplte),
                    PlaceNumber = student.PlaceNumber,
                    MargeType = student.MargeType,
                    SchoolName = school.Name,
                    StudentId = student.Id,
                    StudentName = student.Name,
                    SubjectName = subject.Name,
                    HallNumber = student.HallNumber,
                    QrCode = GenerateQrCodes(Secrite.Id)
                });
            }
            return Data;
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
        private static readonly QRCodeGenerator _qrGenerator = new();
        private byte[] GenerateQrCodes(long code)
        {
            string textCode = $"{code}";
            using var qrData = _qrGenerator.CreateQrCode(textCode, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new PngByteQRCode(qrData);
            return qrCode.GetGraphic(20, new byte[] { 0, 0, 0 }, new byte[] { 255, 255, 255 });
        }
        public async Task<List<ExamTempltesModelData>> GetExamTempltesModel(long schoolId)
        {
            var Models = await context.Student.MaxAsync(p => p.ExamTemplte);
            var MargeTypes = await context.Student.Where(p => p.MargeType != "طبيعي" && p.MargeType != "بصرية").Select(p => p.MargeType).Distinct().ToListAsync();
            var ClassNames = await context.Classes.Where(p => p.SchoolId == schoolId)
                                                  .OrderBy(p => p.ClassType)
                                                  .ThenBy(p => p.Class)
                                                  .ThenBy(p => p.ClassNumber)
                                                  .Select(p => p.Name)
                                                  .Distinct()
                                                  .ToListAsync();
            var data = new List<ExamTempltesModelData>();
            foreach (var name in ClassNames)
            {
                var result = new ExamTempltesModelData();
                result.ClassName = name;
                for (sbyte i = 1; i <= Models; i++)
                {
                    var Mcount = await context.Student.CountAsync(p => p.Class.Name == name && p.Class.SchoolId == schoolId && p.MargeType == "طبيعي" && p.ExamTemplte == i);
                    var Vcount = await context.Student.CountAsync(p => p.Class.Name == name && p.Class.SchoolId == schoolId && p.MargeType == "بصرية" && p.ExamTemplte == i);
                    result.MainModels.Add(new IdNumberNameView()
                    {
                        Name = $"نموزج {Leters(i)}",
                        Id = Mcount
                    });
                    result.VisionModels.Add(new IdNumberNameView()
                    {
                        Name = $"نموزج {Leters(i)}",
                        Id = Vcount
                    });
                }
                foreach (var ty in MargeTypes)
                {
                    var count = await context.Student.CountAsync(p => p.Class.Name == name && p.Class.SchoolId == schoolId && p.MargeType == ty);
                    result.MargeModels.Add(new IdNumberNameView()
                    {
                        Name = $"دمج {ty}",
                        Id = count
                    });
                }
                data.Add(result);
            }
            return data;
        }
    }
}