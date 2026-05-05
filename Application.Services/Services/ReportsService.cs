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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace Application.Services.Services
{
    public class LevelsGrades
    {
        public sbyte level { get; set; }
        public int Review { get; set; }
        public int Behavior { get; set; }
        public int Homework { get; set; }
        public int Activity { get; set; }
        public int Missions { get; set; }
        public int Oral { get; set; }
        public int Classwork { get; set; }
    }
    public class ReportsService : IReportsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;
        private readonly IHelperService helperService;

        public ReportsService(IUnitOfWork _unitOfWork, AppDbContext _context, IHelperService _helperService)
        {
            unitOfWork = _unitOfWork;
            context = _context;
            helperService = _helperService;
        }
        public async Task<AvargeForClassData> GetAverageForClass(StudentForClassScoreView view)
        {
            List<LevelsGrades> LevelsGrades = new();
            LevelsGrades.Add(new LevelsGrades()
            {
                level = 1,
                Classwork = 20,
                Homework = 20,
                Activity = 20,
                Review = 20,
                Oral = 10,
                Missions = 5,
                Behavior = 5
            });
            LevelsGrades.Add(new LevelsGrades()
            {
                level = 2,
                Classwork = 0,
                Homework = 5,
                Activity = 5,
                Review = 5,
                Oral = 0,
                Missions = 10,
                Behavior = 5
            });
            LevelsGrades.Add(new LevelsGrades()
            {
                level = 3,
                Classwork = 0,
                Homework = 10,
                Activity = 0,
                Review = 20,
                Oral = 0,
                Missions = 0,
                Behavior = 10
            });
            LevelsGrades.Add(new LevelsGrades()
            {
                level = 4,
                Classwork = 0,
                Homework = 10,
                Activity = 0,
                Review = 20,
                Oral = 0,
                Missions = 0,
                Behavior = 0
            });
            if (view.ClassId == 0 || view.WeekId == 0 || view.SubjectId == 0) throw new Exception("تاكد من ادخال جميع البيانات");
            var existingScore = await unitOfWork.Repository<StudentScoreData, long>().GetAllNoTrackingAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                WeekId = view.WeekId,
                SubjectId = view.SubjectId,
                ClassId = view.ClassId
            }));
            if (existingScore == null || existingScore.Count() == 0) throw new Exception("لم يتم الرصد بعد");
            sbyte l = HelperFn.GetLevel(existingScore.First().Class.FullId);
            var Grades = LevelsGrades.First(p => p.level == l);
            var result = new AvargeForClassData();
            result.Id = existingScore.First().ClassId;
            result.Name = existingScore.First().Class.FullId;
            result.Level = l;
            foreach (var item in existingScore)
            {
                result.Oral += item.Oral;
                result.Review += item.Review;
                result.Activity += item.Activity;
                result.Classwork += item.Classwork;
                result.Missions += item.Missions;
                result.Behavior += item.Behavior;
                result.Homework += item.Homework;
            }
            result.Oral = Grades.Oral != 0 ? Math.Round(result.Oral / existingScore.Count() / Grades.Oral * 100, 2) : 0;
            result.Review = Grades.Review != 0 ? Math.Round(result.Review / existingScore.Count() / Grades.Review * 100, 2) : 0;
            result.Activity = Grades.Activity != 0 ? Math.Round(result.Activity / existingScore.Count() / Grades.Activity * 100, 2) : 0;
            result.Classwork = Grades.Classwork != 0 ? Math.Round(result.Classwork / existingScore.Count() / Grades.Classwork * 100, 2) : 0;
            result.Missions = Grades.Missions != 0 ? Math.Round(result.Missions / existingScore.Count() / Grades.Missions * 100, 2) : 0;
            result.Behavior = Grades.Behavior != 0 ? Math.Round(result.Behavior / existingScore.Count() / Grades.Behavior * 100, 2) : 0;
            result.Homework = Grades.Homework != 0 ? Math.Round(result.Homework / existingScore.Count() / Grades.Homework * 100, 2) : 0;
            return result;
        }
        private bool CerateScorePdf(IEnumerable<Classes> Classes, string FileName)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);

            IContainer bordercell(IContainer container) => container.Border(1).BorderColor(Colors.Black).PaddingHorizontal(2).PaddingVertical(2);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(8));
                    page.Content()
                    .Padding(1, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(10);
                        foreach (var Class in Classes)
                        {
                            col.Item().Text($"الفصل : {Class.FullId}").FontSize(12).Bold().AlignCenter();
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(5);
                                    c.RelativeColumn(3);
                                });
                                table.Header(h =>
                                {
                                    h.Cell().RowSpan(3).Element(bordercell).AlignCenter().Text("م");
                                    h.Cell().RowSpan(3).Element(bordercell).AlignCenter().AlignMiddle().Text("الاسم");
                                    h.Cell().RowSpan(3).Element(bordercell).AlignCenter().AlignMiddle().Text("الكود");
                                });
                                int i = 1;
                                foreach (var student in Class.Students)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{i}");
                                    table.Cell().Element(bordercell).AlignCenter().Text(student.Name);
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{student.Code}");
                                    i++;
                                }
                            });
                        }
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        private bool CeratePdf(PreparingExamModel data)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, data.ReportName);

            IContainer bordercell(IContainer container) => container.Border(1).BorderColor(Colors.Black).PaddingHorizontal(2).PaddingVertical(2);

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(4, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(12));
                    page.Content()
                    .Padding(1, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(10);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(2);
                                c.RelativeColumn(1);
                                foreach (var item in data.ModelNames)
                                {
                                    c.RelativeColumn(1);
                                }
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("الصف");
                                h.Cell().Element(bordercell).AlignCenter().AlignMiddle().Text("إجمالي الطلاب");
                                foreach (var item in data.ModelNames)
                                {
                                    h.Cell().Element(bordercell).AlignCenter().AlignMiddle().Text(item);
                                }
                                h.Cell().Element(bordercell).AlignCenter().AlignMiddle().Text("دمج");
                                h.Cell().Element(bordercell).AlignCenter().AlignMiddle().Text("دمج بصري");
                            });
                            foreach (var Class in data.Classes)
                            {
                                int i = 0;
                                table.Cell().Element(bordercell).AlignCenter().Text(Class.ClassFullId);
                                table.Cell().Element(bordercell).AlignCenter().Text(Class.TotalStudents.ToString());
                                foreach (var item in Class.Models)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.ToString());
                                }
                                table.Cell().Element(bordercell).AlignCenter().Text(Class.Marge.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(Class.ViewMarge.ToString());
                            }
                            table.Cell().Element(bordercell).AlignCenter().Text("الإجمالي");
                            table.Cell().Element(bordercell).AlignCenter().Text(data.Classes.Sum(p => p.TotalStudents).ToString());
                            for (int i = 0; i < data.ModelNames.Count; i++)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text(data.Classes.Sum(p => p.Models[i]).ToString());
                            }
                            table.Cell().Element(bordercell).AlignCenter().Text(data.Classes.Sum(p => p.Marge).ToString());
                            table.Cell().Element(bordercell).AlignCenter().Text(data.Classes.Sum(p => p.ViewMarge).ToString());

                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        public async Task<ErrorResponce> GetStudentsCode(long SchoolId)
        {
            var classes = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }));
            if (classes == null || classes.Count() == 0) return new ErrorResponce(400, "تم إنشاء الملف", $"StudentsCode_{SchoolId}.pdf");
            bool result = CerateScorePdf(classes, $"StudentsCode_{SchoolId}.pdf");
            if (result) return new ErrorResponce(200, "تم إنشاء الملف", $"StudentsCode_{SchoolId}.pdf");
            return new ErrorResponce(400, "حدث خطأ أثناء إنشاء الملف");
        }
        public async Task<TotalAvrageViewClassData> GetAvrageView(AvrageView view)
        {
            if (view.SchoolId == 0 || string.IsNullOrEmpty(view.ClassName)) throw new Exception("تاكد من ادخال جميع البيانات");
            var TSubjects = await context.TeacherClasses.Where(p => p.Class.Name == view.ClassName &&
                                                                   p.Class.SchoolId == view.SchoolId)
                                                       .Include(p => p.Subject).AsNoTracking()
                                                       .ToListAsync();
            var Subjects = TSubjects.DistinctBy(p => p.SubjectId)
                                                       .Select(p => p.Subject)
                                                       .OrderBy(p => p.Index).ToList();

            var ClassGPA = await context.StudentClassGPA.Where(p => p.Class.Name == view.ClassName &&
                                                              p.StudentId == null &&
                                                              p.Class.SchoolId == view.SchoolId)
                                                       .Include(p => p.Week).Include(p => p.Class)
                                                       .Select(p => new
                                                       {
                                                           Class = new
                                                           {
                                                               ClassType = p.Class.ClassType,
                                                               Class = p.Class.Class,
                                                               ClassNumber = p.Class.ClassNumber,
                                                               FullId = p.Class.FullId,
                                                               Id = p.Class.Id
                                                           },
                                                           WeekName = p.Week.Name,
                                                           SubjectId = p.SubjectId,
                                                           TotalRate = p.TotalRate
                                                       })
                                                       .AsNoTracking()
                                                       .ToListAsync();
            var classes = ClassGPA.DistinctBy(p => p.Class)
                                 .Select(p => p.Class)
                                 .OrderBy(p => p.ClassType)
                                 .ThenBy(p => p.Class)
                                 .ThenBy(p => p.ClassNumber)
                                 .ToList();
            var result = new TotalAvrageViewClassData();
            result.SubjectNames = Subjects.Select(p => p.Name).ToList();
            foreach (var Class in classes)
            {
                var classData = new TotalAvrageViewClassDetails();
                classData.ClassFullId = Class.FullId;
                classData.ClassId = Class.Id;
                foreach (var subject in Subjects)
                {
                    var data = ClassGPA.FirstOrDefault(p => p.SubjectId == subject.Id);
                    classData.Subjects.Add(new TotalAvrageView()
                    {
                        SubjectId = subject.Id,
                        Persent = data == null ? 0 : data.TotalRate,
                        WeekName = data == null ? "" : data.WeekName
                    });
                }
                result.Classes.Add(classData);
            }
            return result;
        }
        public async Task<TotalClassAvrage> GetClassAvrageView(ClassAvrageView view)
        {
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdNoTrackingAsync(new ClassesSpecification(view.ClassId, false));
            var StudentGPA = await context.StudentClassGPA.Where(p => p.ClassId == view.ClassId &&
                                                               p.SubjectId == view.SubjectId &&
                                                               p.StudentId != null)
                                                   .Include(p => p.Student)
                                                   .Select(p => new
                                                   {
                                                       StudentName = p.Student.Name,
                                                       StudentId = p.Student.Id,
                                                       p.Review,
                                                       p.Behavior,
                                                       p.Homework,
                                                       p.Activity,
                                                       p.Missions,
                                                       p.Oral,
                                                       p.Classwork,
                                                       Total = p.TotalRate
                                                   })
                                                   .OrderBy(p => p.StudentName)
                                                   .AsNoTracking()
                                                   .ToListAsync();
            var result = new TotalClassAvrage();
            result.Level = HelperFn.GetLevel(Class.FullId);
            foreach (var Student in StudentGPA)
            {
                result.Students.Add(new TotalClassAvrageDetails()
                {
                    StudentName = Student.StudentName,
                    Activity = Student.Activity,
                    Behavior = Student.Behavior,
                    Homework = Student.Homework,
                    Classwork = Student.Classwork,
                    Missions = Student.Missions,
                    Oral = Student.Oral,
                    Review = Student.Review,
                    Total = Student.Total
                });
            }
            return result;
        }
        public async Task<PreparingExamModel> PreparingExam(PreparingExamView view)
        {
            var Classes = await context.Classes.Where(p => p.Name == view.ClassName &&
                                                        p.SchoolId == view.SchoolId)
                                               .Include(p => p.Students)
                                               .Select(p => new
                                               {
                                                   ClassFullId = p.FullId,
                                                   Students = p.Students.Select(s => new
                                                   {
                                                       Id = s.Id,
                                                       MargeType = s.MargeType
                                                   }).ToList(),
                                               })
                                               .AsNoTracking()
                                               .ToListAsync();
            var result = new PreparingExamModel();
            for (sbyte i = 1; i <= view.Exams; i++)
            {
                result.ModelNames.Add($"نموزج {i}");
            }
            var arr = new short[view.Exams];
            foreach (var Class in Classes)
            {
                for (int i = 0; i < view.Exams; i++)
                {
                    arr[i] = 0;
                }
                var ClassData = new PreparingExamModelClasses();
                ClassData.ClassFullId = Class.ClassFullId;
                ClassData.TotalStudents = Class.Students.Count();
                ClassData.Marge = Class.Students.Count(p => p.MargeType != "طبيعي" && p.MargeType != "بصرية");
                ClassData.ViewMarge = Class.Students.Count(p => p.MargeType == "بصرية");
                int inc = 0;
                foreach (var student in Class.Students.Where(p => p.MargeType == "طبيعي").ToList())
                {
                B:
                    if (inc < view.Exams)
                        arr[inc]++;
                    else
                    {
                        inc = 0;
                        goto B;
                    }
                    inc++;
                }
                for (int i = 0; i < view.Exams; i++)
                {
                    ClassData.Models.Add(arr[i]);
                }
                result.Classes.Add(ClassData);
            }
            result.ReportName = $"{view.ClassName}_{view.SchoolId}.pdf";
            var Created = CeratePdf(result);
            if (Created)
                return result;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<StudentCertificateData> StudentCertificate(StudentCertificateView view)
        {
            var StudentData = await context.StudentMonthlyCertificate.Where(p => p.StudentId == view.StudentId && p.SubjectId == view.SubjectId && p.Month == view.Month)
                                                                     .AsNoTracking()
                                                                     .Include(p => p.Subject)
                                                                     .Include(p => p.Class)
                                                                     .Select(p => new
                                                                     {
                                                                         p.Review,
                                                                         p.Behavior,
                                                                         p.Homework,
                                                                         p.Activity,
                                                                         p.Missions,
                                                                         p.Oral,
                                                                         p.Classwork,
                                                                         p.AllTotal,
                                                                         p.Exam,
                                                                         p.ExamBehavior,
                                                                         p.Total,
                                                                         SubjectName = p.Subject.Name,
                                                                         SubjectStatus = p.Subject.Status,
                                                                         ClassFullId = p.Class.FullId
                                                                     })
                                                                     .ToListAsync();
            if (StudentData == null || StudentData.Count() == 0) throw new Exception("لم يتم تسجيل بيانات الشهادة لهذا الطالب بعد");
            var result = new StudentCertificateData();
            result.Level = HelperFn.GetLevel(StudentData.First().ClassFullId);
            foreach (var item in StudentData)
            {
                result.Subject.Add(new StudentCertificateSubjectData()
                {
                    SubjectName = item.SubjectName,
                    Review = item.Review,
                    Behavior = item.Behavior,
                    Homework = item.Homework,
                    Activity = item.Activity,
                    Missions = item.Missions,
                    Oral = item.Oral,
                    Classwork = item.Classwork,
                    AllTotal = item.AllTotal,
                    Exam = item.Exam,
                    ExamBehavior = item.ExamBehavior,
                    Total = item.Total,
                    SubjectType = item.SubjectStatus
                });
            }
            return result;
        }
        public async Task<ErrorResponce> AddCertificateSetting(CertificateSettingView view)
        {
            var existingData = await context.StudentMonthlyCertificateSetting.AsNoTracking().FirstOrDefaultAsync(p => p.SchoolId == view.SchoolId && (p.WeekId == view.WeekId || p.Month == view.Month) && p.ClassName == view.ClassName);
            if (existingData != null) return new ErrorResponce(404, "تم تسجيل بيانات هذا الفصل لهذا الشهر أو الأسبوع من قبل");
            await unitOfWork.Repository<StudentMonthlyCertificateSetting, long>().AddAsync(new StudentMonthlyCertificateSetting()
            {
                ClassName = view.ClassName,
                SchoolId = view.SchoolId,
                WeekId = view.WeekId,
                Month = view.Month
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ البيانات بنجاح");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteCertificateSetting(long CertificateSettingId)
        {
            var existingData = await context.StudentMonthlyCertificateSetting.FirstOrDefaultAsync(p => p.Id == CertificateSettingId);
            if (existingData == null) return new ErrorResponce(404, "خطأ في حذف , تاكد من ادخال جميع البيانات");
            var Existabs = await context.StudentExamScoreData.AnyAsync(p => p.Class.SchoolId == existingData.SchoolId &&
                                                                            p.Class.Name == existingData.ClassName &&
                                                                            p.Month == existingData.Month);
            if (Existabs) return new ErrorResponce(404, "لا يمكن الحذف بسبب وجود غياب لهاذة الإمتحانات");
            unitOfWork.Repository<StudentMonthlyCertificateSetting, long>().Delete(existingData);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف البيانات بنجاح");
            return new ErrorResponce(400, "خطأ في حذف , تاكد من ادخال جميع البيانات");
        }
        public async Task<List<StudentMonthlyCertificateSettingData>> GetCertificateSettings(long SchoolId)
        {
            var CertificateSetting = await context.StudentMonthlyCertificateSetting.Where(p => p.SchoolId == SchoolId)
                                                                     .Include(p => p.Week)
                                                                     .AsNoTracking()
                                                                     .OrderBy(p => p.Month)
                                                                     .ThenBy(p => p.ClassName)
                                                                     .Select(p => new
                                                                     {
                                                                         Id = p.Id,
                                                                         ClassName = p.ClassName,
                                                                         WeekId = p.WeekId,
                                                                         WeekName = p.Week.Name,
                                                                         Month = p.Month,
                                                                         StartExam = p.StartExam,
                                                                         StartCertificates = p.StartCertificates
                                                                     })
                                                                     .ToListAsync();
            var data = new List<StudentMonthlyCertificateSettingData>();
            foreach (var certSetting in CertificateSetting)
            {
                data.Add(new StudentMonthlyCertificateSettingData()
                {
                    ClassName = certSetting.ClassName,
                    Id = certSetting.Id,
                    Month = DateHelper.MonthNameAr(certSetting.Month),
                    StartCertificates = certSetting.StartCertificates,
                    StartExam = certSetting.StartExam,
                    WeekId = certSetting.WeekId,
                    WeekName = certSetting.WeekName
                });
            }
            if (data == null || data.Count() == 0) throw new Exception("لا توجد بيانات");
            return data;
        }
        public async Task<List<IdNumberNameView>> GetExamsMonths(long SchoolId)
        {
            var CertificateSetting = await context.StudentMonthlyCertificateSetting.Where(p => p.SchoolId == SchoolId)
                                                                     .AsNoTracking()
                                                                     .OrderBy(p => p.Month)
                                                                     .DistinctBy(p => p.Month)
                                                                     .Select(p => new
                                                                     {
                                                                         Month = p.Month,
                                                                     })
                                                                     .ToListAsync();
            var data = new List<IdNumberNameView>();
            foreach (var certSetting in CertificateSetting)
            {
                data.Add(new IdNumberNameView()
                {
                    Id = certSetting.Month,
                    Name = DateHelper.MonthNameAr(certSetting.Month),
                });
            }
            if (data == null || data.Count() == 0) throw new Exception("لا توجد بيانات");
            return data;
        }
        public async Task<AdvErrorResponce<List<string>>> StartExamSave(long CertificateSettingId)
        {
            var existingData = await context.StudentMonthlyCertificateSetting.Include(p => p.Week).FirstOrDefaultAsync(p => p.Id == CertificateSettingId);
            if (existingData == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var Scores = await context.StudentScoreData.Where(p => p.Class.Name == existingData.ClassName &&
                                                                  p.Week.Index <= existingData.Week.Index &&
                                                                  p.Week.IsActive &&
                                                                  p.Class.SchoolId == existingData.SchoolId)
                                                      .Include(p => p.Subject)
                                                      .Select(p => new
                                                      {
                                                          StudentId = p.StudentId,
                                                          WeekId = p.WeekId,
                                                          ClassId = p.ClassId,
                                                          SubjectId = p.SubjectId,
                                                          SubjectName = p.Subject.Name,
                                                          SubjectStatus = p.Subject.Status,
                                                          p.IsPreAbsent,
                                                          p.IsSaved,
                                                          p.IsSeen
                                                      })
                                                      .ToListAsync();
            var Classes = await context.Classes.Where(p => p.Name == existingData.ClassName && p.SchoolId == existingData.SchoolId)
                                               .AsNoTracking()
                                               .Include(p => p.Students)
                                               .OrderBy(p => p.ClassType)
                                               .ThenBy(p => p.Class)
                                               .ThenBy(p => p.ClassNumber)
                                               .ThenBy(p => p.Name)
                                               .Select(p => new
                                               {
                                                   p.Id,
                                                   p.FullId,
                                                   Students = p.Students.Select(p => new
                                                   {
                                                       p.Id,
                                                       p.Name
                                                   }).OrderBy(p => p.Name).ToList()
                                               })
                                               .ToListAsync();
            var Weeks = await context.Week.Where(p => p.IsActive && p.Index <= existingData.Week.Index)
                                          .AsNoTracking()
                                          .Select(p => new
                                          {
                                              p.Id,
                                              p.Name,
                                              p.Index
                                          })
                                          .OrderBy(p => p.Index)
                                          .ToListAsync();
            var Teachers = await context.TeacherClasses.Where(p => p.Class.SchoolId == existingData.SchoolId)
                                                      .AsNoTracking()
                                                      .Include(p => p.Teacher)
                                                      .Select(p => new
                                                      {
                                                          p.SubjectId,
                                                          p.ClassId,
                                                          Teacher = new
                                                          {
                                                              Id = p.TeacherId,
                                                              Name = p.Teacher.Name
                                                          }
                                                      })
                                                      .ToListAsync();
            var QC = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.HasQuiltyControl && p.SchoolId == existingData.SchoolId);
            if (QC == null) throw new Exception("يجب تعيين مراقب جودة");
            var ErrorsList = new List<string>();
            foreach (var Class in Classes)
            {
                foreach (var week in Weeks)
                {
                    foreach (var Subject in Scores.Where(p => p.SubjectStatus != "Off").DistinctBy(p => p.SubjectId).Select(p => p.SubjectId).ToList())
                    {
                        if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject))
                        {
                            if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject && !p.IsSaved))
                            {
                                ErrorsList.Add($"لم يتم رفع درجات الفصل {Class.FullId} للمادة : {Scores.First(p => p.SubjectId == Subject).SubjectName} في الاسبوع {week.Name} عند المدرس {Teachers.FirstOrDefault(p => p.SubjectId == Subject && p.ClassId == Class.Id).Teacher.Name}");
                            }
                            else
                            {
                                foreach (var student in Class.Students)
                                {
                                    if (Scores.Any(p => p.StudentId == student.Id && p.IsPreAbsent && p.WeekId == week.Id && p.SubjectId == Subject))
                                    {
                                        ErrorsList.Add($"الطالب {student.Name} لم يتم تأكيد درجاته للمادة {Scores.First(p => p.SubjectId == Subject).SubjectName} في الاسبوع {week.Name} بسبب غيابه بعزر عند المدرس {Teachers.FirstOrDefault(p => p.SubjectId == Subject && p.ClassId == Class.Id).Teacher.Name}");
                                    }
                                }
                                if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject && p.IsSaved && !p.IsSeen))
                                {
                                    ErrorsList.Add($"لم يتم مراجعة درجات الفصل {Class.FullId} للمادة : {Scores.First(p => p.SubjectId == Subject).SubjectName} في الاسبوع {week.Name} عند مراقب الجودة {QC.Name}");
                                }
                            }
                        }
                        else
                        {
                            ErrorsList.Add($"لا توجد بيانات لصف {Class.FullId} , مادة {Scores.First(p => p.SubjectId == Subject).SubjectName} , أسبوع {week.Name}");
                        }
                    }
                }
            }
            if (ErrorsList.Count() > 0) return new AdvErrorResponce<List<string>>(400, "لا يمكن فتح رصد الإمتحانات بدون مراجعة قائمة الأخطاء", ErrorsList);
            existingData.StartExam = true;
            unitOfWork.Repository<StudentMonthlyCertificateSetting, long>().Update(existingData);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new AdvErrorResponce<List<string>>(200, "تم فتح رصد الامتحانات بنجاح", new());
            throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<AdvErrorResponce<List<string>>> CreateCertificate(long CertificateSettingId)
        {
            var existingData = await context.StudentMonthlyCertificateSetting.Include(p => p.Week).FirstOrDefaultAsync(p => p.Id == CertificateSettingId);
            if (existingData == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var ExamScores = await context.StudentExamScoreData.Where(p => p.Class.Name == existingData.ClassName &&
                                                                  p.Month <= existingData.Month &&
                                                                  p.Class.SchoolId == existingData.SchoolId &&
                                                                  p.IsSeen && p.IsSaved)
                                                           .Include(p => p.Subject)
                                                           .Select(p => new
                                                           {
                                                               StudentId = p.StudentId,
                                                               ClassId = p.ClassId,
                                                               SubjectId = p.SubjectId,
                                                               SubjectName = p.Subject.Name,
                                                               SubjectStatus = p.Subject.Status,
                                                               p.IsAbsent,
                                                               p.IsSaved,
                                                               p.IsSeen,
                                                               p.ExamResult,
                                                               p.Behavior
                                                           })
                                                           .ToListAsync();
            var Subjects = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == existingData.SchoolId && p.Subject.Status != "Off")
                                               .Include(p => p.Subject)
                                               .Select(p => p.Subject)
                                               .Distinct()
                                               .ToListAsync();
            var Classes = await context.Classes.Where(p => p.Name == existingData.ClassName && p.SchoolId == existingData.SchoolId)
                                               .AsNoTracking()
                                               .Include(p => p.Students)
                                               .OrderBy(p => p.ClassType)
                                               .ThenBy(p => p.Class)
                                               .ThenBy(p => p.ClassNumber)
                                               .ThenBy(p => p.Name)
                                               .Select(p => new
                                               {
                                                   p.Id,
                                                   p.FullId,
                                                   Students = p.Students.Select(p => new
                                                   {
                                                       p.Id,
                                                       p.Name
                                                   }).OrderBy(p => p.Name).ToList()
                                               })
                                               .ToListAsync();
            var Teachers = await context.TeacherClasses.Where(p => p.Class.SchoolId == existingData.SchoolId)
                                                      .AsNoTracking()
                                                      .Include(p => p.Teacher)
                                                      .Select(p => new
                                                      {
                                                          p.SubjectId,
                                                          p.ClassId,
                                                          Teacher = new
                                                          {
                                                              Id = p.TeacherId,
                                                              Name = p.Teacher.Name
                                                          }
                                                      })
                                                      .ToListAsync();
            var QC = await context.Teacher.AsNoTracking().FirstOrDefaultAsync(p => p.HasQuiltyControl && p.SchoolId == existingData.SchoolId);
            if (QC == null) throw new Exception("يجب تعيين مراقب جودة");
            var Scores = await context.StudentScoreData.Where(p => p.Class.Name == existingData.ClassName &&
                                                                  p.Week.Index <= existingData.Week.Index &&
                                                                  p.Week.IsActive &&
                                                                  p.Class.SchoolId == existingData.SchoolId)
                                                      .Include(p => p.Subject)
                                                      .Select(p => new
                                                      {
                                                          StudentId = p.StudentId,
                                                          WeekId = p.WeekId,
                                                          ClassId = p.ClassId,
                                                          SubjectId = p.SubjectId,
                                                          SubjectName = p.Subject.Name,
                                                          SubjectStatus = p.Subject.Status,
                                                          p.IsPreAbsent,
                                                          p.IsSaved,
                                                          p.IsSeen
                                                      })
                                                      .ToListAsync();
            var Weeks = await context.Week.Where(p => p.IsActive && p.Index <= existingData.Week.Index)
                                          .AsNoTracking()
                                          .OrderBy(p => p.Index)
                                          .ToListAsync();
            var ErrorsList = new List<string>();
            foreach (var Class in Classes)
            {

                foreach (var Subject in Subjects)
                {
                    foreach (var week in Weeks)
                    {
                        if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject.Id))
                        {
                            if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject.Id && !p.IsSaved))
                            {
                                ErrorsList.Add($"لم يتم رفع درجات الفصل {Class.FullId} للمادة : {Subject.Name} في الاسبوع {week.Name} عند المدرس {Teachers.FirstOrDefault(p => p.SubjectId == Subject.Id && p.ClassId == Class.Id).Teacher.Name}");
                            }
                            else
                            {
                                foreach (var student in Class.Students)
                                {
                                    if (Scores.Any(p => p.StudentId == student.Id && p.IsPreAbsent && p.WeekId == week.Id && p.SubjectId == Subject.Id))
                                    {
                                        ErrorsList.Add($"الطالب {student.Name} لم يتم تأكيد درجاته للمادة {Scores.First(p => p.SubjectId == Subject.Id).SubjectName} في الاسبوع {week.Name} بسبب غيابه بعزر عند المدرس {Teachers.FirstOrDefault(p => p.SubjectId == Subject.Id && p.ClassId == Class.Id).Teacher.Name}");
                                    }
                                }
                                if (Scores.Any(p => p.ClassId == Class.Id && p.WeekId == week.Id && p.SubjectId == Subject.Id && p.IsSaved && !p.IsSeen))
                                {
                                    ErrorsList.Add($"لم يتم مراجعة درجات الفصل {Class.FullId} للمادة : {Scores.First(p => p.SubjectId == Subject.Id).SubjectName} في الاسبوع {week.Name} عند مراقب الجودة {QC.Name}");
                                }
                            }
                        }
                        else
                        {
                            ErrorsList.Add($"لا توجد بيانات لصف {Class.FullId} , مادة {Scores.First(p => p.SubjectId == Subject.Id).SubjectName} , أسبوع {week.Name}");
                        }
                    }
                    if (ExamScores.Any(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id))
                    {
                        if (ExamScores.Any(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id && !p.IsSaved))
                        {
                            ErrorsList.Add($"لم يتم رفع درجات الإمتحان للفصل {Class.FullId} للمادة : {Subject.Name} عند المدرس {Teachers.FirstOrDefault(p => p.SubjectId == Subject.Id && p.ClassId == Class.Id).Teacher.Name}");
                        }
                        else
                        {
                            if (ExamScores.Any(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id && p.IsSaved && !p.IsSeen))
                            {
                                ErrorsList.Add($"لم يتم مراجعة درجات الإمتحان للفصل {Class.FullId} للمادة : {Subject.Name} عند مراقب الجودة {QC.Name}");
                            }
                        }
                    }
                    else
                    {
                        ErrorsList.Add($"لا توجد بيانات لصف {Class.FullId} , مادة {Subject.Name}");
                    }
                }
            }
            if (ErrorsList.Count() > 0) return new AdvErrorResponce<List<string>>(400, "لا يمكن إنشاء الشهادات بدون مراجعة قائمة الأخطاء", ErrorsList);
            var Certificates = await context.StudentMonthlyCertificate.Where(p => p.Class.SchoolId == existingData.SchoolId &&
                                                                              p.Class.Name == existingData.ClassName)
                                                                      .ToListAsync();
            unitOfWork.Repository<StudentMonthlyCertificate, long>().DeleteRange(Certificates);
            var res = await unitOfWork.SaveChangesAsync();
            var OutWeeks = await context.OutOfScore.AsNoTracking()
                                                   .Where(p => p.SchoolId == existingData.SchoolId &&
                                                               p.ClassName == existingData.ClassName)
                                                   .ToListAsync();
            var StudentAbsents = await context.StudentAbsentCases.AsNoTracking().ToListAsync();
            var StScores = await context.StudentScoreData.Where(p => p.Class.Name == existingData.ClassName &&
                                                                  p.Class.SchoolId == existingData.SchoolId &&
                                                                  p.IsSeen && p.IsSaved).ToListAsync();
            foreach (var Class in Classes)
            {
                foreach (var student in Class.Students)
                {
                    foreach (var Subject in Subjects)
                    {
                        var studentWeeks = Weeks.Select(p => new
                        {
                            p.Id,
                            p.EndDate,
                            p.StartDate,
                            p.Index,
                            p.Month,
                            p.Name,
                        }).ToList();
                        var StudentAbsent = StudentAbsents.Where(p => p.StudentId == student.Id).ToList();
                        foreach (var abs in StudentAbsent)
                        {
                            var dates = studentWeeks.Where(w => (abs.StartDate >= w.StartDate && abs.StartDate <= w.EndDate) ||
                                        (abs.EndDate >= w.StartDate && abs.EndDate <= w.EndDate) ||
                                        (abs.StartDate <= w.StartDate && abs.StartDate <= w.EndDate && abs.EndDate >= w.StartDate && abs.EndDate >= w.EndDate)).ToList();
                            foreach (var date in dates) studentWeeks.RemoveAll(p => p.Id == date.Id);
                        }
                        var StudentScores = StScores.Where(p => p.StudentId == student.Id && p.SubjectId == Subject.Id).ToList();
                        var StudentExam = ExamScores.FirstOrDefault(p => p.StudentId == student.Id && p.SubjectId == Subject.Id);
                        var sd = new StudentMonthlyCertificate()
                        {
                            StudentId = student.Id,
                            SubjectId = Subject.Id,
                            Month = existingData.Month,
                            UnderUpdate = false,
                            ClassId = Class.Id,
                            Activity = 0,
                            Behavior = 0,
                            Classwork = 0,
                            Homework = 0,
                            Missions = 0,
                            Review = 0,
                            Oral = 0,
                            Total = 0,
                            Exam = StudentExam == null ? 0 : StudentExam.ExamResult,
                            ExamBehavior = 0,
                            AllTotal = 0,
                        };
                        if (studentWeeks.Count() == 0)
                        {
                            sd.AllTotal = Math.Round(sd.Total + sd.Exam + sd.ExamBehavior, 2);
                            await unitOfWork.Repository<StudentMonthlyCertificate, long>().AddAsync(sd);
                            continue;
                        }
                        int ReviewInc = 0;
                        foreach (var week in studentWeeks)
                        {
                            var ss = StudentScores.FirstOrDefault(p => p.WeekId == week.Id);
                            if (ss == null) continue;
                            sd.Activity += ss.Activity;
                            sd.Behavior += ss.Behavior;
                            sd.Classwork += ss.Classwork;
                            sd.Homework += ss.Homework;
                            sd.Missions += ss.Missions;
                            sd.Oral += ss.Oral;
                            if (!OutWeeks.Any(p => p.WeekId == week.Id))
                            {
                                sd.Review += ss.Review;
                                ReviewInc++;
                            }
                        }
                        sd.Activity = Math.Round(sd.Activity / studentWeeks.Count(), 2);
                        sd.Behavior = Math.Round(sd.Behavior / studentWeeks.Count(), 2);
                        sd.Homework = Math.Round(sd.Homework / studentWeeks.Count(), 2);
                        sd.Missions = Math.Round(sd.Missions / studentWeeks.Count(), 2);
                        sd.Oral = Math.Round(sd.Oral / studentWeeks.Count(), 2);
                        sd.Classwork = Math.Round(sd.Classwork / studentWeeks.Count(), 2);
                        if (ReviewInc > 0) sd.Review = Math.Round(sd.Review / ReviewInc, 2);
                        else sd.Review = 0;
                        sd.Total += Math.Round(sd.Activity + sd.Behavior + sd.Classwork + sd.Homework + sd.Missions + sd.Review + sd.Oral, 2);
                        sd.AllTotal = Math.Round(sd.Total + sd.Exam + sd.ExamBehavior, 2);
                        await unitOfWork.Repository<StudentMonthlyCertificate, long>().AddAsync(sd);
                    }
                }
            }
            existingData.StartCertificates = true;
            unitOfWork.Repository<StudentMonthlyCertificateSetting, long>().Update(existingData);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new AdvErrorResponce<List<string>>(200, "تم إنشاء الشهادات بنجاح", new());
            throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        //للإعدادي فقط
        public async Task<CertificateReportData> CertificateReport(CertificateReportView view)
        {
            var Certificates = await context.StudentMonthlyCertificate.AsNoTracking()
                                                                      .Where(p => p.Class.SchoolId == view.SchoolId && p.Month == view.Month)
                                                                      .Include(p => p.Class)
                                                                      .ToListAsync();
            if (Certificates.Count == 0) throw new Exception("لا يوجد إحصائيات لهذا الشهر");
            var ClassNames = await helperService.GetClassesNames(view.SchoolId);
            var Classes = await context.Classes.AsNoTracking().Where(p => p.SchoolId == view.SchoolId).ToListAsync();
            var AllTc = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == view.SchoolId && p.Subject.Status != "Off")
                                               .Include(p => p.Subject)
                                               .ToListAsync();
            var Subjects = AllTc.DistinctBy(p => p.SubjectId).Select(p => p.Subject).OrderBy(p => p.Index).ToList();
            var data = new CertificateReportData();
            data.SubjectName = Subjects.Select(p => p.Name).ToList();
            foreach (var ClassName in ClassNames)
            {
                var ClassNameRecord = new CertificateReportClassesData()
                {
                    ClassName = ClassName
                };
                var ClassNameData = Certificates.Where(p => p.Class.Name == ClassName).ToList();
                if (ClassNameData.Count == 0) continue;
                foreach (var Subject in Subjects)
                {
                    ClassNameRecord.TotalPersent.Add(Math.Round(ClassNameData.Where(p => p.SubjectId == Subject.Id &&
                                                                                    p.AllTotal >= 27.5).Count() /
                                                                                    (double)ClassNameData.Count(p => p.SubjectId == Subject.Id) * 100, 2));
                }
                foreach (var Class in Classes.Where(p => p.Name == ClassName).ToList())
                {
                    var ClassRecord = new ReportClassData()
                    {
                        ClassFullId = Class.FullId
                    };
                    foreach (var Subject in Subjects)
                    {
                        ClassRecord.TotalPersent.Add(Math.Round(ClassNameData.Where(p => p.SubjectId == Subject.Id && p.AllTotal >= 27.5 && p.ClassId == Class.Id).Count() / (double)ClassNameData.Count(p => p.SubjectId == Subject.Id && p.ClassId == Class.Id) * 100, 2));
                    }
                    ClassNameRecord.ClassData.Add(ClassRecord);
                }
                data.ClassesData.Add(ClassNameRecord);
            }
            return data;
        }
        //للإعدادي فقط
        public async Task<ClassDigramData> ClassDigram(ClassDigramView view)
        {
            var ClassDetails = await context.Classes.AsNoTracking()
                                                    .Select(p => new
                                                    {
                                                        p.Id,
                                                        p.Name,
                                                        p.FullId,
                                                        p.SchoolId
                                                    }).FirstOrDefaultAsync();
            if (ClassDetails == null) throw new Exception("خطأ في البيانات");
            var ClassData = await context.Student.AsNoTracking()
                                                 .Where(p => p.ClassId == view.ClassId)
                                                 .Select(p => new
                                                 {
                                                     p.Id,
                                                     p.Name
                                                 })
                                                 .ToListAsync();
            if (ClassData.Count == 0) throw new Exception("خطأ في البيانات");
            var ClassScoreData = await context.StudentScoreData.AsNoTracking()
                                                               .Where(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId)
                                                               .Include(p => p.Week)
                                                               .OrderBy(p => p.WeekId)
                                                               .Select(p => new
                                                               {
                                                                   p.Behavior,
                                                                   p.Homework,
                                                                   p.Review,
                                                                   p.SubjectId,
                                                                   p.ClassId,
                                                                   p.WeekId,
                                                                   p.StudentId,
                                                                   WeekName = p.Week.Name
                                                               })
                                                               .ToListAsync();
            if (ClassScoreData.Count == 0) throw new Exception("خطأ في البيانات");
            var OutWeeks = await context.OutOfScore.AsNoTracking()
                                                   .Where(p => p.SchoolId == ClassDetails.SchoolId &&
                                                               p.ClassName == ClassDetails.Name &&
                                                               p.SubjectId == view.SubjectId)
                                                   .ToListAsync();
            var Weeks = await context.Week.Where(p => p.IsActive)
                                          .AsNoTracking()
                                          .OrderBy(p => p.Index)
                                          .ToListAsync();
            var StudentAbsents = await context.StudentAbsentCases.AsNoTracking()
                                                                 .Where(p => p.Student.ClassId == view.ClassId)
                                                                 .ToListAsync();
            var data = new ClassDigramData()
            {
                ClassFullId = ClassDetails.FullId,
                ClassId = ClassDetails.Id,
                Level = HelperFn.GetLevel(ClassDetails.FullId)
            };
            foreach (var student in ClassData)
            {
                var studentWeeks = Weeks.Select(p => new
                {
                    p.Id,
                    p.EndDate,
                    p.StartDate,
                    p.Index,
                    p.Month,
                    p.Name,
                }).ToList();
                var StudentAbsent = StudentAbsents.Where(p => p.StudentId == student.Id).ToList();
                foreach (var abs in StudentAbsent)
                {
                    var dates = studentWeeks.Where(w => (abs.StartDate >= w.StartDate && abs.StartDate <= w.EndDate) ||
                                (abs.EndDate >= w.StartDate && abs.EndDate <= w.EndDate) ||
                                (abs.StartDate <= w.StartDate && abs.StartDate <= w.EndDate && abs.EndDate >= w.StartDate && abs.EndDate >= w.EndDate)).ToList();
                    foreach (var date in dates) studentWeeks.RemoveAll(p => p.Id == date.Id);
                }
                var ss = new StudenDigramDatat()
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    Progress = 0,
                };
                int WeekIndex = 1;
                foreach (var Sweek in studentWeeks)
                {
                    var StudentScores = ClassScoreData.FirstOrDefault(p => p.StudentId == student.Id && p.WeekId == Sweek.Id);
                    if (StudentScores == null) continue;
                    var StScore = StudentScores.Homework + StudentScores.Behavior;
                    if (!OutWeeks.Any(p => p.WeekId == Sweek.Id)) StScore += StudentScores.Review;
                    var ScoreRecord = new DigramData()
                    {
                        WeekName = Sweek.Name,
                        WeekId = WeekIndex++,
                        Score = StScore
                    };
                    ss.Scores.Add(ScoreRecord);

                }
                if (ss.Scores.Count >= 3)
                {
                    ss.Progress = Math.Round(((ss.Scores.Last().Score) - (ss.Scores.Take(ss.Scores.Count - 1).Sum(p => p.Score) / (ss.Scores.Count - 1))) * 2.5, 2);
                }
                data.Students.Add(ss);
            }
            int CWeekIndex = 1;
            foreach (var week in Weeks)
            {
                var Cscore = data.Students.Select(p => p.Scores.Where(p => p.WeekId == week.Id));
                var Scores = new DigramData()
                {
                    WeekName = week.Name,
                    WeekId = CWeekIndex++,
                    Score = Math.Round(Cscore.Sum(p => p.Sum(x => x.Score)) / ClassData.Count, 2)
                };
                data.Scores.Add(Scores);
            }
            return data;
        }

    }
}