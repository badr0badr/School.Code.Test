﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.TekStatics;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Diagnostics;

namespace Application.Services.Services
{
    public class TekStaticsService : ITekStaticsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IScoreService scoreService;
        private readonly AppDbContext context;

        public TekStaticsService(IUnitOfWork _unitOfWork, IScoreService _ScoreService, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            scoreService = _ScoreService;
            context = _context;
        }
        private bool CerateScorePdf(List<string> Students, List<string> Weeks, string FileName, string ClassFullId, string SchoolName)
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
                                .PaddingHorizontal(2)
                                .PaddingVertical(2);
            }
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(6));
                    page.Header()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(6);
                                c.RelativeColumn(5);
                                c.RelativeColumn(1);
                            });
                            //table.Cell().Image(imgbyts2).FitArea();

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
                            table.Cell().AlignBottom().AlignRight().Text($"          سجل رصد درجات فصل {ClassFullId}             المادة...........").FontSize(10);
                            //table.Cell().Text("");
                            table.Cell().Image(imgbyts).FitArea();
                            //table.Cell().ColumnSpan(3).Text($"          سجل رصد درجات فصل {ClassFullId}             المادة...........").AlignCenter().Bold().FontSize(10);
                        });
                    page.Footer()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("مدرس المادة").FontSize(9);
                            table.Cell().AlignCenter().Text("مشرف المادة").FontSize(9);
                            table.Cell().AlignCenter().Text("شئون الطلاب").FontSize(9);
                            table.Cell().AlignCenter().Text("مدير المدرسة").FontSize(9);
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                        });
                    page.Content()
                    .Padding(1, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(10);
                        col.Item().Table(table =>
                        {
                            //data
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(8);
                                for (int i = 0; i < Weeks.Count() * 4; i++)
                                {
                                    c.RelativeColumn(1);
                                }
                            });
                            table.Header(h =>
                            {
                                h.Cell().RowSpan(3).Element(bordercell).AlignCenter().Text("م");
                                h.Cell().RowSpan(3).Element(bordercell).AlignCenter().AlignMiddle().Text("الاسم");
                                for (int i = 0; i < Weeks.Count; i++)
                                {
                                    h.Cell().ColumnSpan(4).Element(bordercell).AlignCenter().Text(Weeks[i]);
                                }
                                for (int i = 0; i < Weeks.Count(); i++)
                                {
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("تقييم أسبوعى");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الواجبات المنزلية");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المواظبة و السلوك");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المجموع");
                                }
                                for (int i = 0; i < Weeks.Count(); i++)
                                {
                                    h.Cell().Element(bordercell).AlignCenter().Text("20");
                                    h.Cell().Element(bordercell).AlignCenter().Text("10");
                                    h.Cell().Element(bordercell).AlignCenter().Text("10");
                                    h.Cell().Element(bordercell).AlignCenter().Text("40");
                                }
                            });
                            for (int i = 0; i < Students.Count; i++)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text((i + 1).ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(Students[i]).FontSize(7);
                                for (int j = 0; j < Weeks.Count; j++)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                }
                            }
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        private bool CerateAbsentPdf(List<string> Students, List<string> Weeks, string FileName, string ClassFullId, string SchoolName)
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
                                .PaddingHorizontal(2)
                                .PaddingVertical(2);
            }
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.ContentFromRightToLeft();
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(6));
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
                            table.Cell().AlignBottom().AlignRight().Text($"          سجل غياب فصل {ClassFullId}             المادة...........").FontSize(10);
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Footer()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("مدرس المادة").FontSize(9);
                            table.Cell().AlignCenter().Text("مشرف المادة").FontSize(9);
                            table.Cell().AlignCenter().Text("شئون الطلاب").FontSize(9);
                            table.Cell().AlignCenter().Text("مدير المدرسة").FontSize(9);
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                            table.Cell().ColumnSpan(4).AlignCenter().Text(" ");
                        });
                    page.Content()
                    .Padding(1, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(10);
                        col.Item().Table(table =>
                        {
                            //data
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(8);
                                for (int i = 0; i < Weeks.Count() * 6; i++)
                                {
                                    c.RelativeColumn(1);
                                }
                            });
                            table.Header(h =>
                            {
                                h.Cell().RowSpan(2).Element(bordercell).AlignCenter().Text("م");
                                h.Cell().RowSpan(2).Element(bordercell).AlignCenter().AlignMiddle().Text("الاسم");
                                for (int i = 0; i < Weeks.Count; i++)
                                {
                                    h.Cell().ColumnSpan(6).Element(bordercell).AlignCenter().Text(Weeks[i]);
                                }
                                for (int i = 0; i < Weeks.Count(); i++)
                                {
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("السبت");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الأحد");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الإثنين");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الثلاثاء");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الأربعاء");
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الخميس");
                                }
                            });
                            for (int i = 0; i < Students.Count; i++)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text((i + 1).ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(Students[i]).FontSize(7);
                                for (int j = 0; j < Weeks.Count; j++)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                    table.Cell().Element(bordercell).AlignCenter().Text("");
                                }
                            }
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        public async Task<ClassSheetPdf> GetClassSheets(ClassSheetPdfView view)
        {
            var data = new ClassSheetPdf();
            var Students = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = view.ClassId,
                IsDeleted = false
            }));
            var Weeks = await unitOfWork.Repository<Week, long>().GetAllAsync(new WeekSpecification(new WeekParams()
            {
                Month = view.Month
            }));
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdAsync(new ClassesSpecification(view.ClassId));
            data.ScoreSheet = $"{view.ClassId}_ScoreSheet.pdf";
            var ScorePdf = CerateScorePdf(Students.Select(p => p.Name).ToList(), Weeks.Select(p => p.Name).ToList(), data.ScoreSheet, Class.FullId, Class.School.Name);
            data.AbsentSheet = $"{view.ClassId}_AbsentSheet.pdf";
            var AbsentPdf = CerateAbsentPdf(Students.Select(p => p.Name).ToList(), Weeks.Select(p => p.Name).ToList(), data.AbsentSheet, Class.FullId, Class.School.Name);
            return data;
        }
        public async Task<Get5BehaviorView> Get5Behavior(BehaviorView view)
        {
            var Result = new Get5BehaviorView();
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdAsync(new ClassesSpecification(view.ClassId));
            var Studentabsentcases = await unitOfWork.Repository<StudentAbsentCases, long>().GetAllAsync(new StudentAbsentCasesSpecification());
            Result.Month = view.Month;
            Result.ClassId = view.ClassId;
            Result.ClassFullId = Class.FullId;
            var Students = await unitOfWork.Repository<Student, long>().GetAllAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = view.ClassId
            }));

            var Schoolvacations = await unitOfWork.Repository<SchoolVacation, long>().GetAllAsync(new SchoolVacationSpecification(new SchoolVacationParams()
            {
                SchoolId = Class.SchoolId
            }));
            var Absent = await unitOfWork.Repository<StudentAbsentData, long>().GetAllAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                ClassId = view.ClassId,
                ClassNumber = 1,
            }));
            var week = await unitOfWork.Repository<Week, long>().GetByIdAsync(new WeekSpecification(view.Month));
            foreach (var student in Students)
            {
                var TotalAbs = 0;
                var STrecord = new Student5BehaviorView();
                STrecord.StudentName = student.Name;
                STrecord.StudentId = student.Id;
                var startday = week.StartDate;
                while (week.EndDate >= startday)
                {
                    var dayrecord = new StudentDayDetails();
                    dayrecord.Day = startday.Day;
                    dayrecord.Name = DateHelper.DayNameAr(startday.DayOfWeek.ToString());
                    var studentCase = Studentabsentcases.FirstOrDefault(p => p.StudentId == student.Id && p.StartDate <= startday && p.EndDate >= startday);
                    var dayschoolvacation = Schoolvacations.FirstOrDefault(p => p.StartDate <= startday && p.EndDate >= startday);
                    if (dayschoolvacation != null)
                    {
                        dayrecord.IsExist = 4; // (-) bg-Yellow
                        dayrecord.Reason = dayschoolvacation.Reason; ;
                    }
                    else if (dayrecord.Name == "الجمعة" || dayrecord.Name == "السبت")
                    {
                        dayrecord.IsExist = 4; // (-) bg-Yellow
                        dayrecord.Reason = "";
                    }
                    else if (Absent == null)
                    {
                        dayrecord.IsExist = 5; // (-) bg-White
                        dayrecord.Reason = "";
                    }
                    else if (Absent.Count() == 0)
                    {
                        dayrecord.IsExist = 5; // (-) bg-White
                        dayrecord.Reason = "";
                    }
                    else if (studentCase != null)
                    {
                        dayrecord.IsExist = 2; // (o) bg-Yellow
                        dayrecord.Reason = studentCase.Reason;
                    }
                    else
                    {
                        var StudentAbsentstep = Absent.Where(p => p.Date.Day == startday.Day);
                        var StudentAbsent = StudentAbsentstep != null ? StudentAbsentstep.FirstOrDefault(p => p.StudentId == student.Id) : null;
                        if (StudentAbsentstep == null)
                        {
                            dayrecord.IsExist = 5; // (-) bg-White
                            dayrecord.Reason = "";
                        }
                        else if (startday > DateOnly.FromDateTime(DateTime.Now))
                        {
                            dayrecord.IsExist = 5; // (-) bg-White
                            dayrecord.Reason = "";
                        }
                        else if (StudentAbsent != null)
                        {
                            if (StudentAbsent.Reason == "" && StudentAbsent.IsAbsent == false)
                            {
                                dayrecord.IsExist = 3; // (✓) bg-Green
                                dayrecord.Reason = "";
                            }
                            else if (StudentAbsent.Reason != "" && StudentAbsent.IsAbsent == false)
                            {
                                dayrecord.IsExist = 2; // (o) bg-Yellow
                                dayrecord.Reason = StudentAbsent.Reason ?? "";
                            }
                            else if (StudentAbsent.IsAbsent == true)
                            {
                                dayrecord.IsExist = 0; // (x) bg-Red
                                dayrecord.Reason = StudentAbsent.Reason ?? "";
                                TotalAbs++;
                            }
                        }
                        else
                        {
                            dayrecord.IsExist = 3;// (✓) bg-Green   
                            dayrecord.Reason = "";
                        }
                    }
                    STrecord.studentDays.Add(dayrecord);
                    startday = startday.AddDays(1);
                }
                STrecord.studentDays.Add(new StudentDayDetails()
                {
                    Day = view.Month,
                    IsExist = -3,
                    Name = "إجمالي",
                    Reason = TotalAbs.ToString()
                });
                Result.Students.Add(STrecord);
            }
            return Result;
            // throw new NotImplementedException("لا يوجد اسابيع لهذا الشهر");
        }
        public async Task<MonitorGradesDetails> MonitorGrades(MonitorGradesView view)
        {
            var result = new MonitorGradesDetails();
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId,
                Name = view.Grade
            }, false));
            var TCs = await unitOfWork.Repository<TeacherClasses, long>().GetAllNoTrackingAsync(new TeacherClassesSpecification());
            var StudentData = await unitOfWork.Repository<StudentScoreData, long>().GetAllNoTrackingAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                WeekId = view.WeekId
            }, false));
            var Subjects = TCs.Where(p => p.Class.Name == view.Grade && p.Class.SchoolId == view.SchoolId && p.Subject.Status != "Off").DistinctBy(p => p.SubjectId).OrderBy(p => p.Subject.Index).Select(p => p.Subject).ToList();
            result.Subjects = Subjects.Select(p => p.Name).ToList();
            foreach (var Class in Classes)
            {
                var ClassRecord = new MonitorGradesDetailsClass();
                ClassRecord.ClassId = Class.Id;
                ClassRecord.ClassName = Class.FullId;
                foreach (var Subject in Subjects)
                {
                    bool IsSeen = true;
                    bool IsSaved = true;

                    var SubjectRecord = new MonitorGradesDetailsSubject();
                    SubjectRecord.SubjectId = Subject.Id;
                    var data = StudentData.Where(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id).ToList();
                    if (data.Any(p => !p.IsSeen)) IsSeen = false;
                    if (data.Any(p => !p.IsSaved)) IsSaved = false;

                    if (data.Count() == 0) SubjectRecord.Status = -1;
                    else if (IsSeen) SubjectRecord.Status = 1;
                    else if (IsSaved) SubjectRecord.Status = 0;
                    else SubjectRecord.Status = -1;
                    ClassRecord.Subjects.Add(SubjectRecord);
                }
                result.Classes.Add(ClassRecord);
            }
            return result;
        }
        public async Task<MonitorGradesDataDetails> MonitorGradesData(StudentForClassScoreView view)
        {
            var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                ClassId = view.ClassId,
                SubjectId = view.SubjectId,
            }));
            var result = new MonitorGradesDataDetails()
            {
                ClassFullId = TC.Class.FullId,
                ClassId = TC.ClassId,
                Level = HelperFn.GetLevel(TC.Class.FullId),
                SubjectId = TC.SubjectId,
                SubjectName = TC.Subject.Name,
                TeacherId = TC.TeacherId,
                TeacherName = TC.Teacher.Name
            };
            result.Scores = await scoreService.GetStudentForClassScore(view);
            return result;
        }
        public async Task<ErrorResponce> RestMonitorGrades(StudentForClassScoreView view)
        {
            var existingScore = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                WeekId = view.WeekId,
                SubjectId = view.SubjectId,
                ClassId = view.ClassId
            }));
            foreach (var score in existingScore)
            {
                existingScore.First(p => p.Id == score.Id).IsSaved = false;
            }
            unitOfWork.Repository<StudentScoreData, long>().UpdateRange(existingScore);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم إرجاع الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في إرجاع , تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ConfirmMonitorGrades(StudentForClassScoreView view)
        {
            var existingScore = await context.StudentScoreData.Where(p => p.WeekId == view.WeekId &&
                                                                          p.SubjectId == view.SubjectId &&
                                                                          p.ClassId == view.ClassId)
                                                              .Include(p => p.Class)
                                                              .ToListAsync();
            foreach (var score in existingScore)
            {
                existingScore.First(p => p.Id == score.Id).IsSeen = true;
            }
            unitOfWork.Repository<StudentScoreData, long>().UpdateRange(existingScore);
            LevelsGrades Grades = new();
            sbyte l = HelperFn.GetLevel(existingScore.First().Class.FullId);
            if (l == 1) Grades = new LevelsGrades()
            {
                level = 1,
                Classwork = 20,
                Homework = 20,
                Activity = 20,
                Review = 20,
                Oral = 10,
                Missions = 5,
                Behavior = 5
            };
            else if (l == 2) Grades = new LevelsGrades()
            {
                level = 2,
                Classwork = 0,
                Homework = 5,
                Activity = 5,
                Review = 5,
                Oral = 0,
                Missions = 10,
                Behavior = 5
            };
            else if (l == 3) Grades = new LevelsGrades()
            {
                level = 3,
                Classwork = 0,
                Homework = 10,
                Activity = 0,
                Review = 20,
                Oral = 0,
                Missions = 0,
                Behavior = 10
            };
            else Grades = new LevelsGrades()
            {
                level = 4,
                Classwork = 0,
                Homework = 10,
                Activity = 0,
                Review = 20,
                Oral = 0,
                Missions = 0,
                Behavior = 0
            };
            long SchoolId = existingScore.First().Class.SchoolId;
            var GPA = await context.StudentClassGPA.Where(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId).ToListAsync();
            var StudentIds = await context.Student.Where(p => p.ClassId == view.ClassId).AsNoTracking().Select(p => p.Id).ToListAsync();
            if (StudentIds.Count() == 0) return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var AllSdata = await context.StudentScoreData.Where(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId && (p.IsSeen == true || p.WeekId == view.WeekId))
                                                         .Include(p => p.Week)
                                                         .AsNoTracking()
                                                         .Select(p => new
                                                         {
                                                             p.StudentId,
                                                             WeekIndex = p.Week.Index,
                                                             p.WeekId,
                                                             p.Activity,
                                                             p.Behavior,
                                                             p.Homework,
                                                             p.Missions,
                                                             p.Classwork,
                                                             p.Oral,
                                                             p.Review,
                                                         })
                                                         .ToListAsync();
            if (AllSdata.Count() == 0) return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var LastWeekId = AllSdata.DistinctBy(p => p.WeekId).OrderByDescending(p => p.WeekIndex).First().WeekId;
            var Weeks = await context.Week.AsNoTracking().Where(p => p.IsActive).ToListAsync();
            var outweeks = await context.OutOfScore.AsNoTracking()
                                                 .Where(p => p.SchoolId == SchoolId &&
                                                             p.SubjectId == view.SubjectId &&
                                                             p.ClassName == existingScore.First().Class.Name)
                                                 .ToListAsync();
            var StudentsAbsent = await context.StudentAbsentCases.AsNoTracking()
                                                                 .Where(p => p.Student.ClassId == view.ClassId)
                                                                 .ToListAsync();
            var RestWeeks = new List<Week>();
            foreach (var week in Weeks)
            {
                if (outweeks.FirstOrDefault(p => p.WeekId == week.Id) == null) RestWeeks.Add(week);
            }
            List<StudentDataAvarge> StudentsData = new();
            var ClassGPA = GPA.FirstOrDefault(p => p.ClassId == view.ClassId && p.StudentId == null);
            if (ClassGPA == null)
                ClassGPA = new StudentClassGPA()
                {
                    Id = 0,
                    ClassId = view.ClassId,
                    SubjectId = view.SubjectId,
                    StudentId = null
                };
            foreach (var studentId in StudentIds)
            {
                int ReviewSkip = 0;
                var sd = new StudentDataAvarge()
                {
                    StudentId = studentId
                };
                foreach (var week in RestWeeks)
                {
                    var StudentScore = AllSdata.FirstOrDefault(p => p.StudentId == studentId && p.WeekId == week.Id);
                    if (StudentScore == null) continue;
                    var studentAbsent = StudentsAbsent.Where(p => p.StudentId == studentId).ToList();
                    if (studentAbsent.Any((abs => (abs.StartDate >= week.StartDate && abs.StartDate <= week.EndDate) ||
                                    (abs.EndDate >= week.StartDate && abs.EndDate <= week.EndDate) ||
                                    (abs.StartDate <= week.StartDate && abs.StartDate <= week.EndDate && abs.EndDate >= week.StartDate && abs.EndDate >= week.EndDate))))
                    {
                        sd.Scores.Add(new StudentDataAvargeScores()
                        {
                            Activity = StudentScore.Activity,
                            Behavior = StudentScore.Behavior,
                            Classwork = StudentScore.Classwork,
                            Homework = StudentScore.Homework,
                            Missions = StudentScore.Missions,
                            Oral = StudentScore.Oral
                        });
                        ReviewSkip++;
                    }
                    else
                        sd.Scores.Add(new StudentDataAvargeScores()
                        {
                            Activity = StudentScore.Activity,
                            Behavior = StudentScore.Behavior,
                            Classwork = StudentScore.Classwork,
                            Homework = StudentScore.Homework,
                            Missions = StudentScore.Missions,
                            Oral = StudentScore.Oral,
                            Review = StudentScore.Review,
                        });
                }
                sd.Review = sd.Scores.Count() - ReviewSkip <= 0 ? 0 : Math.Round(sd.Scores.Sum(p => p.Review) / (sd.Scores.Count() - ReviewSkip), 2);
                sd.Oral = Math.Round(sd.Scores.Sum(p => p.Oral) / sd.Scores.Count(), 2);
                sd.Activity = Math.Round(sd.Scores.Sum(p => p.Activity) / sd.Scores.Count(), 2);
                sd.Behavior = Math.Round(sd.Scores.Sum(p => p.Behavior) / sd.Scores.Count(), 2);
                sd.Homework = Math.Round(sd.Scores.Sum(p => p.Homework) / sd.Scores.Count(), 2);
                sd.Missions = Math.Round(sd.Scores.Sum(p => p.Missions) / sd.Scores.Count(), 2);
                sd.Classwork = Math.Round(sd.Scores.Sum(p => p.Classwork) / sd.Scores.Count(), 2);
                sd.TotalRate = sd.Activity + sd.Behavior + sd.Classwork + sd.Homework + sd.Missions + sd.Oral + sd.Review;
                StudentsData.Add(sd);
                var SGPA = GPA.FirstOrDefault(p => p.StudentId == studentId);
                if (SGPA != null)
                {
                    SGPA.Activity = sd.Activity;
                    SGPA.Behavior = sd.Behavior;
                    SGPA.Missions = sd.Missions;
                    SGPA.Homework = sd.Homework;
                    SGPA.Review = sd.Review;
                    SGPA.Oral = sd.Oral;
                    SGPA.Classwork = sd.Classwork;
                    SGPA.TotalRate = sd.TotalRate;
                    SGPA.WeeKId = LastWeekId;
                    unitOfWork.Repository<StudentClassGPA, long>().Update(SGPA);
                }
                else
                {
                    SGPA = new StudentClassGPA()
                    {
                        Activity = sd.Activity,
                        Behavior = sd.Behavior,
                        Classwork = sd.Classwork,
                        Oral = sd.Oral,
                        Homework = sd.Homework,
                        Missions = sd.Missions,
                        Review = sd.Review,
                        TotalRate = sd.TotalRate,
                        StudentId = studentId,
                        SubjectId = view.SubjectId,
                        ClassId = view.ClassId,
                        WeeKId = LastWeekId
                    };
                    await unitOfWork.Repository<StudentClassGPA, long>().AddAsync(SGPA);
                }
            }
            ClassGPA.Activity = Grades.Activity == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Activity) / StudentIds.Count() / Grades.Activity * 100, 2);
            ClassGPA.Behavior = Grades.Behavior == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Behavior) / StudentIds.Count() / Grades.Behavior * 100, 2);
            ClassGPA.Classwork = Grades.Classwork == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Classwork) / StudentIds.Count() / Grades.Classwork * 100, 2);
            ClassGPA.Homework = Grades.Homework == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Homework) / StudentIds.Count() / Grades.Homework * 100, 2);
            ClassGPA.Missions = Grades.Missions == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Missions) / StudentIds.Count() / Grades.Missions * 100, 2);
            ClassGPA.Oral = Grades.Oral == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Oral) / StudentIds.Count() / Grades.Oral * 100, 2);
            ClassGPA.Review = Grades.Review == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Review) / StudentIds.Count() / Grades.Review * 100, 2);

            ClassGPA.TotalRate = Grades.Activity + Grades.Behavior + Grades.Classwork + Grades.Homework +
                                 Grades.Missions + Grades.Oral + Grades.Review == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.TotalRate)
                                 / StudentIds.Count() / (Grades.Activity + Grades.Behavior + Grades.Classwork + Grades.Homework +
                                 Grades.Missions + Grades.Oral + Grades.Review) * 100, 2);
            ClassGPA.WeeKId = LastWeekId;
            if (ClassGPA.Id == 0)
                await unitOfWork.Repository<StudentClassGPA, long>().AddAsync(ClassGPA);
            else
                unitOfWork.Repository<StudentClassGPA, long>().Update(ClassGPA);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }
        public async Task<MonitorGradesDetails> GetMonitorExamGrades(StudentForExamGradeView view)
        {
            var result = new MonitorGradesDetails();
            var Classes = await context.Classes.AsNoTracking()
                                             .Where(p => p.SchoolId == view.SchoolId)
                                             .OrderBy(p => p.ClassType)
                                             .ThenBy(p => p.Class)
                                             .ThenBy(p => p.ClassNumber)
                                             .Select(p => new
                                             {
                                                 p.Id,
                                                 p.FullId
                                             })
                                             .ToListAsync();
            var AllTc = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == view.SchoolId && p.Subject.Status != "Off")
                                               .Include(p => p.Subject)
                                               .ToListAsync();
            var Subjects = AllTc.DistinctBy(p => p.SubjectId).Select(p => p.Subject).ToList();
            var StudentData = await context.StudentExamScoreData.AsNoTracking()
                                                            .Where(p => p.Class.SchoolId == view.SchoolId && p.Month == view.Month)
                                                            .Select(p => new
                                                            {
                                                                p.ClassId,
                                                                p.SubjectId,
                                                                p.IsSaved,
                                                                p.IsSeen
                                                            })
                                                            .ToListAsync();
            if (StudentData.Count() == 0) throw new Exception("لا يوجد بيانات لهذا الشهر");
            result.Subjects = Subjects.Select(p => p.Name).ToList();
            foreach (var Class in Classes)
            {
                var ClassRecord = new MonitorGradesDetailsClass();
                ClassRecord.ClassId = Class.Id;
                ClassRecord.ClassName = Class.FullId;
                foreach (var Subject in Subjects)
                {
                    bool IsSeen = true;
                    bool IsSaved = true;
                    var SubjectRecord = new MonitorGradesDetailsSubject();
                    SubjectRecord.SubjectId = Subject.Id;
                    var data = StudentData.Where(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id).ToList();
                    foreach (var score in data)
                    {
                        if (score.IsSeen) continue;
                        else IsSeen = false;
                        if (score.IsSaved) continue;
                        else IsSaved = false;
                    }
                    if (data.Count() == 0) SubjectRecord.Status = -1;
                    else if (IsSeen) SubjectRecord.Status = 1;
                    else if (IsSaved) SubjectRecord.Status = 0;
                    else SubjectRecord.Status = -1;
                    ClassRecord.Subjects.Add(SubjectRecord);
                }
                result.Classes.Add(ClassRecord);
            }
            return result;
        }
        public async Task<MonitotExamGradesDataDetails> MonitorExamGradesData(StudentForExamScoreView view)
        {
            var TC = await context.TeacherClasses.AsNoTracking()
                                                .Include(p => p.Teacher)
                                                .Include(p => p.Subject)
                                                .Include(p => p.Class)
                                                .FirstOrDefaultAsync(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId);
            if (TC == null) throw new Exception("خطأ في الحفظ , تاكد من ادخال جميع البيانات");
            var existingScore = await context.StudentExamScoreData.AsNoTracking()
                                                                  .Where(p => p.Month == view.Month &&
                                                                          p.SubjectId == view.SubjectId &&
                                                                          p.ClassId == view.ClassId)
                                                                  .Include(p => p.Student)
                                                                  .OrderBy(p => p.Student.Name)
                                                                  .Select(p => new StudentExamScoreDData()
                                                                  {
                                                                      Name = p.Student.Name,
                                                                      Exam = p.ExamResult,
                                                                      PreAbsent = p.IsAbsent
                                                                  })
                                                                  .ToListAsync();
            var result = new MonitotExamGradesDataDetails()
            {
                ClassFullId = TC.Class.FullId,
                ClassId = TC.ClassId,
                SubjectId = TC.SubjectId,
                SubjectName = TC.Subject.Name,
                TeacherId = TC.TeacherId,
                TeacherName = TC.Teacher.Name,
                Scores = existingScore
            };
            return result;
        }
        public async Task<ErrorResponce> RestMonitorExamGrades(StudentForExamScoreView view)
        {
            var existingScore = await unitOfWork.Repository<StudentExamScoreData, long>().GetAllAsync(new StudentExamScoreDataSpecification(new StudentExamScoreDataParams()
            {
                Month = view.Month,
                SubjectId = view.SubjectId,
                ClassId = view.ClassId
            }, false));
            foreach (var score in existingScore)
            {
                existingScore.First(p => p.Id == score.Id).IsSaved = false;
            }
            unitOfWork.Repository<StudentExamScoreData, long>().UpdateRange(existingScore);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم إرجاع الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في إرجاع , تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ConfirmMonitorExamGrades(StudentForExamScoreView view)
        {
            var existingScore = await context.StudentExamScoreData.AsNoTracking()
                                                                  .Where(p => p.Month == view.Month &&
                                                                          p.SubjectId == view.SubjectId &&
                                                                          p.ClassId == view.ClassId)
                                                                  .ToListAsync();
            foreach (var score in existingScore)
            {
                existingScore.First(p => p.Id == score.Id).IsSeen = true;
            }
            unitOfWork.Repository<StudentExamScoreData, long>().UpdateRange(existingScore);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الدرجات بنجاح");
            return new ErrorResponce(400, "خطأ في الحفظ , تاكد من ادخال جميع البيانات");
        }











        public async Task<List<StudentClassGPA>> TestConfirm(long subjectId)
        {
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = 23,
            }, false));
            foreach (var Class in Classes)
            {
                var data = await saveGrade(new StudentForClassScoreView()
                {
                    ClassId = Class.Id,
                    SubjectId = subjectId,
                    WeekId = 37
                });
                if (data.Count() != 0)
                {
                    return data;
                }
            }
            return new();
        }
        private async Task<List<StudentClassGPA>> saveGrade(StudentForClassScoreView view)
        {
            LevelsGrades Grades = new LevelsGrades()
            {
                level = 3,
                Classwork = 0,
                Homework = 10,
                Activity = 0,
                Review = 20,
                Oral = 0,
                Missions = 0,
                Behavior = 10
            };
            long SchoolId = 23;
            var Class = await context.Classes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == view.ClassId);
            if (Class == null) return new();
            var GPA = await context.StudentClassGPA.Where(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId).ToListAsync();
            var StudentIds = await context.Student.Where(p => p.ClassId == view.ClassId).AsNoTracking().Select(p => p.Id).ToListAsync();
            var AllSdata = await context.StudentScoreData.Where(p => p.ClassId == view.ClassId && p.SubjectId == view.SubjectId && p.IsSeen == true)
                                                         .Include(p => p.Week)
                                                         .AsNoTracking()
                                                         .Select(p => new
                                                         {
                                                             StudentId = p.StudentId,
                                                             WeekIndex = p.Week.Index,
                                                             WeekId = p.WeekId,
                                                             Activity = p.Activity,
                                                             Behavior = p.Behavior,
                                                             Homework = p.Homework,
                                                             Missions = p.Missions,
                                                             Classwork = p.Classwork,
                                                             Oral = p.Oral,
                                                             Review = p.Review,
                                                         })
                                                         .ToListAsync();
            if (AllSdata.Count() == 0) return new();
            var LastWeekId = AllSdata.DistinctBy(p => p.WeekId).OrderByDescending(p => p.WeekIndex).First().WeekId;
            var Weeks = await context.Week.AsNoTracking().Where(p => p.IsActive).ToListAsync();
            var outweeks = await context.OutOfScore.AsNoTracking()
                                                 .Where(p => p.SchoolId == SchoolId &&
                                                             p.SubjectId == view.SubjectId &&
                                                             p.ClassName == Class.Name)
                                                 .ToListAsync();
            var StudentsAbsent = await context.StudentAbsentCases.AsNoTracking()
                                                                 .Where(p => p.Student.ClassId == view.ClassId)
                                                                 .ToListAsync();
            var RestWeeks = new List<Week>();
            foreach (var week in Weeks)
            {
                if (outweeks.FirstOrDefault(p => p.WeekId == week.Id) == null) RestWeeks.Add(week);
            }
            List<StudentDataAvarge> StudentsData = new();
            List<StudentClassGPA> errordata = new();
            var ClassGPA = GPA.FirstOrDefault(p => p.ClassId == view.ClassId && p.StudentId == null);
            if (ClassGPA == null)
                ClassGPA = new StudentClassGPA()
                {
                    Id = 0,
                    ClassId = view.ClassId,
                    SubjectId = view.SubjectId,
                    StudentId = null
                };
            foreach (var studentId in StudentIds)
            {
                //if (studentId == 721)
                //{
                //    int x = 0;
                //}
                int ReviewSkip = 0;
                var sd = new StudentDataAvarge()
                {
                    StudentId = studentId
                };
                foreach (var week in RestWeeks)
                {
                    var StudentScore = AllSdata.FirstOrDefault(p => p.StudentId == studentId && p.WeekId == week.Id);
                    if (StudentScore == null) continue;
                    var studentAbsent = StudentsAbsent.Where(p => p.StudentId == studentId).ToList();
                    if (studentAbsent.Any((abs => (abs.StartDate >= week.StartDate && abs.StartDate <= week.EndDate) ||
                                    (abs.EndDate >= week.StartDate && abs.EndDate <= week.EndDate) ||
                                    (abs.StartDate <= week.StartDate && abs.StartDate <= week.EndDate && abs.EndDate >= week.StartDate && abs.EndDate >= week.EndDate))))
                    {
                        sd.Scores.Add(new StudentDataAvargeScores()
                        {
                            Activity = StudentScore.Activity,
                            Behavior = StudentScore.Behavior,
                            Classwork = StudentScore.Classwork,
                            Homework = StudentScore.Homework,
                            Missions = StudentScore.Missions,
                            Oral = StudentScore.Oral
                        });
                        ReviewSkip++;
                    }
                    else
                        sd.Scores.Add(new StudentDataAvargeScores()
                        {
                            Activity = StudentScore.Activity,
                            Behavior = StudentScore.Behavior,
                            Classwork = StudentScore.Classwork,
                            Homework = StudentScore.Homework,
                            Missions = StudentScore.Missions,
                            Oral = StudentScore.Oral,
                            Review = StudentScore.Review,
                        });
                }
                sd.Review = sd.Scores.Count() - ReviewSkip <= 0 ? 0 : Math.Round(sd.Scores.Sum(p => p.Review) / (sd.Scores.Count() - ReviewSkip), 2);
                sd.Oral = Math.Round(sd.Scores.Sum(p => p.Oral) / sd.Scores.Count(), 2);
                sd.Activity = Math.Round(sd.Scores.Sum(p => p.Activity) / sd.Scores.Count(), 2);
                sd.Behavior = Math.Round(sd.Scores.Sum(p => p.Behavior) / sd.Scores.Count(), 2);
                sd.Homework = Math.Round(sd.Scores.Sum(p => p.Homework) / sd.Scores.Count(), 2);
                sd.Missions = Math.Round(sd.Scores.Sum(p => p.Missions) / sd.Scores.Count(), 2);
                sd.Classwork = Math.Round(sd.Scores.Sum(p => p.Classwork) / sd.Scores.Count(), 2);
                sd.TotalRate = sd.Activity + sd.Behavior + sd.Classwork + sd.Homework + sd.Missions + sd.Oral + sd.Review;
                StudentsData.Add(sd);
                var SGPA = GPA.FirstOrDefault(p => p.StudentId == studentId);
                if (SGPA != null)
                {
                    SGPA.Activity = sd.Activity;
                    SGPA.Behavior = sd.Behavior;
                    SGPA.Missions = sd.Missions;
                    SGPA.Homework = sd.Homework;
                    SGPA.Review = sd.Review;
                    SGPA.Oral = sd.Oral;
                    SGPA.Classwork = sd.Classwork;
                    SGPA.TotalRate = sd.TotalRate;
                    SGPA.WeeKId = LastWeekId;
                    unitOfWork.Repository<StudentClassGPA, long>().Update(SGPA);
                    errordata.Add(SGPA);
                }
                else
                {
                    SGPA = new StudentClassGPA()
                    {
                        Activity = sd.Activity,
                        Behavior = sd.Behavior,
                        Classwork = sd.Classwork,
                        Oral = sd.Oral,
                        Homework = sd.Homework,
                        Missions = sd.Missions,
                        Review = sd.Review,
                        TotalRate = sd.TotalRate,
                        StudentId = studentId,
                        SubjectId = view.SubjectId,
                        ClassId = view.ClassId,
                        WeeKId = LastWeekId
                    };
                    await unitOfWork.Repository<StudentClassGPA, long>().AddAsync(SGPA);
                    errordata.Add(SGPA);
                }
            }
            if (StudentIds.Count() == 0) return new();
            ClassGPA.Activity = Grades.Activity == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Activity) / StudentIds.Count() / Grades.Activity * 100, 2);
            ClassGPA.Behavior = Grades.Behavior == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Behavior) / StudentIds.Count() / Grades.Behavior * 100, 2);
            ClassGPA.Classwork = Grades.Classwork == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Classwork) / StudentIds.Count() / Grades.Classwork * 100, 2);
            ClassGPA.Homework = Grades.Homework == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Homework) / StudentIds.Count() / Grades.Homework * 100, 2);
            ClassGPA.Missions = Grades.Missions == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Missions) / StudentIds.Count() / Grades.Missions * 100, 2);
            ClassGPA.Oral = Grades.Oral == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Oral) / StudentIds.Count() / Grades.Oral * 100, 2);
            ClassGPA.Review = Grades.Review == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.Review) / StudentIds.Count() / Grades.Review * 100, 2);

            ClassGPA.TotalRate = Grades.Activity + Grades.Behavior + Grades.Classwork + Grades.Homework +
                                 Grades.Missions + Grades.Oral + Grades.Review == 0 ? 0 : Math.Round(StudentsData.Sum(p => p.TotalRate)
                                 / StudentIds.Count() / (Grades.Activity + Grades.Behavior + Grades.Classwork + Grades.Homework +
                                 Grades.Missions + Grades.Oral + Grades.Review) * 100, 2);

            ClassGPA.WeeKId = LastWeekId;
            if (ClassGPA.Id == 0)
                await unitOfWork.Repository<StudentClassGPA, long>().AddAsync(ClassGPA);
            else
                unitOfWork.Repository<StudentClassGPA, long>().Update(ClassGPA);
            errordata.Add(ClassGPA);
            try
            {
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new();
            }
            catch
            {
                return errordata;
            }
            throw new Exception("faild");
        }
    }
}