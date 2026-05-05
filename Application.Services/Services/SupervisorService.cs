﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class SupervisorService : ISupervisorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public SupervisorService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        #region PDF
        private bool CeratePdf(List<TitelsShareView> data, string FileName, string header, string SchoolName)
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
                            table.Cell().AlignBottom().AlignRight().Text(header).FontSize(10);
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الوظيفة").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("النصاب القانوني").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("عدد المعلمين").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("إجمالي النصاب القانوني").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("إجمالي النصاب الفعلي").Bold();
                            });
                            int index = 1;
                            foreach (var item in data)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text(index.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Title);
                                table.Cell().Element(bordercell).AlignCenter().Text(item.LegalShare.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.TotalTeachers.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.TotalLegalShare.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.TotalCurrentShare.ToString());
                                index++;
                            }
                        });
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("شئون عاملين ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        private bool CeratePdf(List<CSharePowerView> data, string FileName, string header, string SchoolName)
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
                            table.Cell().AlignBottom().AlignRight().Text(header).FontSize(10);
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(2);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الصف").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("نصاب الفصل").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("عدد الفصول").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("إجمالي").Bold();


                            });
                            int index = 1;
                            foreach (var item in data)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text(index.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.ClassName);
                                table.Cell().Element(bordercell).AlignCenter().Text(item.LegalShare.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.ClassNumber.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text((item.ClassNumber * item.LegalShare).ToString());
                                index++;
                            }
                        });
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("شئون عاملين ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        private bool CeratePdf(SubjectMapDetails data, string FileName, string header, string SchoolName)
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
                                c.RelativeColumn(2);
                                c.RelativeColumn(4);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignBottom().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                });
                                table.Cell().AlignRight().Text("مديرية التربية والتعليم بالمنوفية").FontSize(14);
                                table.Cell().AlignRight().Text("إدارة سرس الليان التعليمية").FontSize(14);
                                table.Cell().AlignRight().Text($"مدرسة {SchoolName}").FontSize(14);
                            });
                            table.Cell().AlignBottom().AlignCenter().Text(header).FontSize(16);
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(3);
                                c.RelativeColumn(2);
                                c.RelativeColumn(3);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("اسم المعلم").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الوظيفة").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الفصول").Bold();
                                h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("النصاب القانوني").Bold();
                                h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("النصاب الفعلي").Bold();
                                h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("العجز والزيادة").Bold();


                            });
                            int index = 1;
                            foreach (var item in data.TMainMap)
                            {
                                string classes = "";
                                foreach (var cl in item.Classes)
                                    classes += $"{cl} , ";
                                if (classes.Length > 1)
                                    classes = classes.Remove(classes.Length - 2);
                                table.Cell().Element(bordercell).AlignCenter().Text($"{index}");
                                table.Cell().Element(bordercell).AlignCenter().Text(item.TeacherName);
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Title);
                                table.Cell().Element(bordercell).AlignCenter().Text(classes);
                                table.Cell().Element(bordercell).AlignCenter().Text($"{item.LegalShare}");
                                table.Cell().Element(bordercell).AlignCenter().Text($"{item.CurrentShare}");
                                table.Cell().Element(bordercell).AlignCenter().Text($"{item.Result}");
                                index++;
                            }
                            table.Cell().ColumnSpan(3).Element(bordercell).AlignCenter().Text("الإجمالي");
                            table.Cell().Element(bordercell).AlignCenter().Text($"{data.TotalClasses}");
                            table.Cell().Element(bordercell).AlignCenter().Text($"{data.TotalLegalShare}");
                            table.Cell().Element(bordercell).AlignCenter().Text($"{data.TotalCurrentShare}");
                            table.Cell().Element(bordercell).AlignCenter().Text($"{data.TotalResult}");
                        });
                        col.Item().AlignCenter().Text(data.Report);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("شئون عاملين ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        #endregion
        public async Task<List<TeacherTitleView>> GetAllTeacherTitles()
        {
            var Titles = await unitOfWork.Repository<TeacherTitle, long>().GetAllAsync(new TeacherTitleSpecification());
            return Titles.Select(p => new TeacherTitleView()
            {
                Id = p.Id,
                Title = p.Title,
                ShareAmount1_2 = p.ShareAmount1_2,
                ShareAmount3 = p.ShareAmount3,
                ShareAmount4 = p.ShareAmount4
            }).ToList();
        }
        public async Task<List<ClassSubjectShareView>> GetAllClassSubjectShare()
        {
            var Shares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification());
            var Subjects = await unitOfWork.Repository<Subject, long>().GetAllAsync(new SubjectSpecification());
            if (Shares.Count() == 0) throw new Exception("لم يتم تسجيل البيانات بعد");
            var result = new List<ClassSubjectShareView>();
            foreach (var subject in Subjects)
            {
                var record = new ClassSubjectShareView();
                record.SubjectId = subject.Id;
                record.SubjectName = subject.Name;
                foreach (var Share in Shares.Where(p => p.SubjectId == subject.Id))
                {
                    record.ShareData.Add(new ClassSubjectShareViewData()
                    {
                        ClassName = Share.ClassName,
                        ShareAmount = Share.ShareAmount
                    });
                }
                result.Add(record);
            }
            return result;
        }
        public async Task<ClassSubjectShareView> GetClassesShareForSubject(long SubjectId)
        {
            var Shares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification(new ClassSubjectShareParams()
            {
                SubjectId = SubjectId
            }));
            if (Shares.Count() == 0) throw new Exception("لم يتم تسجيل البيانات بعد");
            if (Shares.FirstOrDefault() == null) throw new Exception("لم يتم تسجيل البيانات بعد");
            var record = new ClassSubjectShareView();
            record.SubjectId = Shares.FirstOrDefault().SubjectId;
            record.SubjectName = Shares.FirstOrDefault().Subject.Name;
            foreach (var Share in Shares)
            {
                record.ShareData.Add(new ClassSubjectShareViewData()
                {
                    ClassName = Share.ClassName,
                    ShareAmount = Share.ShareAmount
                });
            }
            return record;
        }
        public async Task<SubjectClassShareView> GetSubjectShareForClass(string ClassName)
        {
            var Shares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification(new ClassSubjectShareParams()
            {
                ClassName = ClassName
            }));
            if (Shares.Count() == 0) throw new Exception("لم يتم تسجيل البيانات بعد");
            if (Shares.FirstOrDefault() == null) throw new Exception("لم يتم تسجيل البيانات بعد");
            var record = new SubjectClassShareView();
            record.ClassName = ClassName;
            foreach (var Share in Shares.OrderBy(p => p.Subject.Index))
            {
                record.ShareData.Add(new SubjectClassShareViewData()
                {
                    SubjectName = Share.Subject.Name,
                    ShareAmount = Share.ShareAmount
                });
            }
            return record;
        }
        public async Task<ErrorResponce> AddOrUpdateClassSubjectShare(AddOrUpdateClassSubjectShareView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            var Shares = await unitOfWork.Repository<ClassSubjectShare, long>().GetByIdAsync(new ClassSubjectShareSpecification(new ClassSubjectShareParams()
            {
                ClassName = view.ClassName,
                SubjectId = view.SubjectId
            }));
            if (Shares == null)
            {
                await unitOfWork.Repository<ClassSubjectShare, long>().AddAsync(new ClassSubjectShare()
                {
                    ClassName = view.ClassName,
                    SubjectId = view.SubjectId,
                    ShareAmount = view.ShareAmount
                });
            }
            else
            {
                Shares.ShareAmount = view.ShareAmount;
                unitOfWork.Repository<ClassSubjectShare, long>().Update(Shares);
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<SchoolSharePowerView> SchoolSharePower(long SchoolId)
        {
            var result = new SchoolSharePowerView();
            var Teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId
            }));
            var AssiTeachers = await unitOfWork.Repository<Assignment, long>().GetAllAsync(new AssignmentSpecification(new AssignmentParams()
            {
                SchoolId = SchoolId
            }));
            Teachers = Teachers.Where(p => p.RoleId == "TeacherSuper" || p.RoleId == "Teacher" || p.RoleId2 == "TeacherSuper" || p.RoleId2 == "Teacher").ToList();
            foreach (var teacher in Teachers)
            {
                var record = new SharePowerView();
                if (teacher.TeacherTitle == null) continue;
                if (teacher.Grade == "الإبتدائى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount1_2;
                else if (teacher.Grade == "الإعدادى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount3;
                else if (teacher.Grade == "الثانوى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount4;
                record.TeacherName = teacher.Name;
                record.Title = teacher.TeacherTitle.Title;
                record.TitleDate = teacher.TeacherTitleDate;
                result.TotalShare += record.LegalShare;
                result.TShare.Add(record);
            }
            foreach (var teacher in AssiTeachers)
            {
                var record = new SharePowerView();
                record.TeacherName = teacher.Teacher.Name;
                record.Title = "منتدب";
                result.TShare.Add(record);
            }
            return result;
        }
        public async Task<ClassSharePowerView> ClassSharePower(long SchoolId)
        {
            var result = new ClassSharePowerView();
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }));
            var ClassSubjectShares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification());
            foreach (var Class in Classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
            {
                var record = new CSharePowerView();
                record.ClassName = Class.Name;
                record.LegalShare = ClassSubjectShares.Where(p => p.ClassName == Class.Name).Sum(p => p.ShareAmount);
                result.TotalShare += record.LegalShare;
                result.TShare.Add(record);
            }
            return result;
        }
        public async Task<SummaryShareView> SummaryShare(long SchoolId)
        {
            var result = new SummaryShareView();
            var classShare = await ClassSharePower(SchoolId);
            var schoolShare = await SchoolSharePower(SchoolId);
            result.TotalClassShare = classShare.TotalShare;
            result.TotalTeacherShare = schoolShare.TotalShare;
            result.Result = result.TotalTeacherShare - result.TotalClassShare;
            if (result.Result > 0)
            {
                result.Report = "فائض في حصة المعلمين عن حصة الفصول";
            }
            else if (result.Result < 0)
            {
                result.Report = "عجز في حصة المعلمين عن حصة الفصول";
            }
            else
            {
                result.Report = "تساوي في حصة المعلمين عن حصة الفصول";
            }
            return result;
        }
        public async Task<List<SummaryShareDetailsView>> SummaryShareDetails(long SchoolId)
        {
            var result = new List<SummaryShareDetailsView>();
            var Subjects = await unitOfWork.Repository<Subject, long>().GetAllAsync(new SubjectSpecification());
            var share = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification());
            foreach (var subject in Subjects)
            {
                var record = new SummaryShareDetailsView();
                record.Subject = subject.Name;
                var classShare = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
                {
                    SchoolId = SchoolId
                }));
                foreach (var Class in classShare.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
                {
                    var subShare = share.FirstOrDefault(p => p.ClassName == Class.Name && p.SubjectId == subject.Id);
                    if (subShare != null)
                    {
                        record.TotalClassShare += subShare.ShareAmount;
                    }
                }
                var schoolShare = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
                {
                    SchoolId = SchoolId,
                    MainSubjectId = subject.Id
                }));
                foreach (var teacher in schoolShare)
                {
                    if (teacher.TeacherTitle == null) continue;
                    if (teacher.Grade == "الإبتدائى")
                        record.TotalTeacherShare += teacher.TeacherTitle.ShareAmount1_2;
                    else if (teacher.Grade == "الإعدادى")
                        record.TotalTeacherShare += teacher.TeacherTitle.ShareAmount3;
                    else if (teacher.Grade == "الثانوى")
                        record.TotalTeacherShare += teacher.TeacherTitle.ShareAmount4;
                }
                record.Result = record.TotalTeacherShare - record.TotalClassShare;
                if (record.Result > 0) record.Report = "فائض";
                else if (record.Result < 0) record.Report = "عجز";
                else record.Report = "متزن";
            }
            return result;
        }
        public async Task<SubjectSummaryShareDetails> SubjectSummaryShareDetails(SubjectSummaryShareDetailsView view)
        {
            var result = new SubjectSummaryShareDetails();
            var classShare = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }));
            var ClassSubjectShares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification(new ClassSubjectShareParams()
            {
                SubjectId = view.SubjectId
            }));
            var Teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = view.SchoolId,
                MainSubjectId = view.SubjectId
            }));
            foreach (var Class in ClassSubjectShares)
            {
                var record = new CSharePowerView();
                record.ClassName = Class.ClassName;
                record.LegalShare = Class.ShareAmount * classShare.Where(p => p.Name == Class.ClassName).Count();
                result.TotalClassShare += record.LegalShare;
                result.ClassesShare.Add(record);
            }
            foreach (var teacher in Teachers)
            {
                var record = new SharePowerView();
                if (teacher.TeacherTitle == null) continue;
                if (teacher.Grade == "الإبتدائى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount1_2;
                else if (teacher.Grade == "الإعدادى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount3;
                else if (teacher.Grade == "الثانوى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount4;
                record.TeacherName = teacher.Name;
                record.Title = teacher.TeacherTitle.Title;
                result.TotalTeacherShare += record.LegalShare;
                result.TeachersShare.Add(record);
            }
            return result;
        }
        ///////////////////////
        public async Task<List<IdNumberNameView>> GetTeachers(GetTeachersVisionView view)
        {
            if (view == null) throw new Exception("يجب ادخال جميع البيانات");
            if (view.SuperType == "daily")
            {
                var Skipteachers = await unitOfWork.Repository<GeneralSupervision, long>().GetAllAsync(new GeneralSupervisionSpecification());
                var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
                {
                    SchoolId = view.SchoolId
                }));
                teachers = teachers.Where(p => p.RoleId == "Teacher" || p.RoleId == "TeacherSuper" || p.RoleId == "Worker" || p.RoleId == "Statics").ToList();
                if (Skipteachers.Count() > 0)
                {
                    foreach (var item in Skipteachers)
                    {
                        teachers = teachers.Where(p => p.Id != item.TeacherId).ToList();
                    }
                }
                return teachers.OrderBy(p => p.Name).Select(p => new IdNumberNameView()
                {
                    Name = p.Name,
                    Id = p.Id
                }).ToList();
            }
            else if (view.SuperType == "general")
            {
                var Skipteachers = await unitOfWork.Repository<DailySupervision, long>().GetAllAsync(new DailySupervisionSpecification());
                Skipteachers = Skipteachers.Where(p => p.SchoolId == view.SchoolId).ToList();
                var Tskip = Skipteachers.Where(p => p.Saturday != null).Select(p => p.Saturday).ToList();
                Tskip.AddRange(Skipteachers.Where(p => p.Sunday != null).Select(p => p.Sunday).ToList());
                Tskip.AddRange(Skipteachers.Where(p => p.Monday != null).Select(p => p.Monday).ToList());
                Tskip.AddRange(Skipteachers.Where(p => p.Tuseday != null).Select(p => p.Tuseday).ToList());
                Tskip.AddRange(Skipteachers.Where(p => p.Wednesday != null).Select(p => p.Wednesday).ToList());
                Tskip.AddRange(Skipteachers.Where(p => p.Thursday != null).Select(p => p.Thursday).ToList());
                Tskip = Tskip.DistinctBy(p => p).ToList();
                var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
                {
                    SchoolId = view.SchoolId
                }));
                teachers = teachers.Where(p => p.RoleId == "Teacher" || p.RoleId == "TeacherSuper" || p.RoleId == "Worker" || p.RoleId == "Statics").ToList();
                if (Skipteachers.Count() > 0)
                {
                    foreach (var item in Tskip)
                    {
                        teachers = teachers.Where(p => p.Id != item).ToList();
                    }
                }
                return teachers.OrderBy(p => p.Name).Select(p => new IdNumberNameView()
                {
                    Name = p.Name,
                    Id = p.Id
                }).ToList();

            }
            throw new Exception("يجب ادخال جميع البيانات");
        }
        public async Task<List<DailySupervisionView>> DailySupervision(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<DailySupervision, long>().GetAllAsync(new DailySupervisionSpecification());
            return teachers.Where(p => p.SchoolId == SchoolId).Select(p => new DailySupervisionView()
            {
                ClassId = p.Class.FullId,
                Day1 = p.TSaturday != null ? p.TSaturday.Name : "",
                Day2 = p.TSunday != null ? p.TSunday.Name : "",
                Day3 = p.TMonday != null ? p.TMonday.Name : "",
                Day4 = p.TTuseday != null ? p.TTuseday.Name : "",
                Day5 = p.TWednesday != null ? p.TWednesday.Name : "",
                Day6 = p.TThursday != null ? p.TThursday.Name : "",
            }).ToList();
        }
        public async Task<List<IdStringNameView>> GeneralSupervision(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<GeneralSupervision, long>().GetAllAsync(new GeneralSupervisionSpecification());
            return teachers.Where(p => p.SchoolId == SchoolId).Select(p => new IdStringNameView()
            {
                Id = p.Teacher.Name,
                Name = DateHelper.DayNameAr(p.Day)
            }).ToList();
            throw new NotImplementedException();

        }
        public async Task<ErrorResponce> ModifySupervision(ModifySupervisionView view)
        {
            if (view == null) return new ErrorResponce(400, "يجب ادخال جميع البيانات");
            if (view.SuperType == "daily")
            {
                var supervisions = await unitOfWork.Repository<DailySupervision, long>().GetAllAsync(new DailySupervisionSpecification());
                var supervision = supervisions.FirstOrDefault(p => p.ClassId == view.ClassId);
                if (supervision == null)
                {
                    var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.TeacherId));
                    var newSupervision = new DailySupervision()
                    {
                        ClassId = view.ClassId,
                        SchoolId = teacher.SchoolId,
                    };
                    if (view.Day == "Saturday")
                        newSupervision.Saturday = view.TeacherId;
                    else if (view.Day == "Sunday")
                        newSupervision.Sunday = view.TeacherId;
                    else if (view.Day == "Monday")
                        newSupervision.Monday = view.TeacherId;
                    else if (view.Day == "Tuesday")
                        newSupervision.Tuseday = view.TeacherId;
                    else if (view.Day == "Wednesday")
                        newSupervision.Wednesday = view.TeacherId;
                    else if (view.Day == "Thursday")
                        newSupervision.Thursday = view.TeacherId;
                    await unitOfWork.Repository<DailySupervision, long>().AddAsync(newSupervision);
                    var result = await unitOfWork.SaveChangesAsync();
                    if (result > 0) return new ErrorResponce(200, "تم بنجاح");
                }
                else
                {
                    if (view.Day == "Saturday")
                        supervision.Saturday = view.TeacherId;
                    else if (view.Day == "Sunday")
                        supervision.Sunday = view.TeacherId;
                    else if (view.Day == "Monday")
                        supervision.Monday = view.TeacherId;
                    else if (view.Day == "Tuesday")
                        supervision.Tuseday = view.TeacherId;
                    else if (view.Day == "Wednesday")
                        supervision.Wednesday = view.TeacherId;
                    else if (view.Day == "Thursday")
                        supervision.Thursday = view.TeacherId;
                    unitOfWork.Repository<DailySupervision, long>().Update(supervision);
                    var result = await unitOfWork.SaveChangesAsync();
                    if (result > 0) return new ErrorResponce(200, "تم بنجاح");
                }
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            }
            else if (view.SuperType == "general")
            {
                var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.TeacherId));
                var supervisions = await unitOfWork.Repository<GeneralSupervision, long>().GetAllAsync(new GeneralSupervisionSpecification());
                var supervision = supervisions.FirstOrDefault(p => p.Day == view.Day && p.SchoolId == teacher.SchoolId);
                if (supervision == null)
                {
                    var newSupervision = new GeneralSupervision()
                    {
                        Day = view.Day,
                        TeacherId = view.TeacherId,
                        SchoolId = teacher.SchoolId
                    };
                    await unitOfWork.Repository<GeneralSupervision, long>().AddAsync(newSupervision);
                    var result = await unitOfWork.SaveChangesAsync();
                    if (result > 0) return new ErrorResponce(200, "تم بنجاح");
                }
                else
                {
                    supervision.TeacherId = view.TeacherId;
                    unitOfWork.Repository<GeneralSupervision, long>().Update(supervision);
                    var result = await unitOfWork.SaveChangesAsync();
                    if (result > 0) return new ErrorResponce(200, "تم بنجاح");
                }
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            }
            return new ErrorResponce(400, "يجب ادخال جميع البيانات");
        }
        ///////////////////////-
        public async Task<SubjectMapDetails> SubjectMap(SubjectMapView view)
        {
            if (view == null) throw new Exception("يجب ادخال جميع البيانات");
            var data = new SubjectMapDetails();
            data.ReportName = $"الخطة {view.SchoolId} {view.SubjectId}.pdf";
            var Shares = await context.ClassSubjectShare.AsNoTracking()
                                                        .Include(p => p.Subject)
                                                        .Where(p => p.SubjectId == view.SubjectId)
                                                        .Select(p => new
                                                        {
                                                            p.ClassName,
                                                            p.ShareAmount,
                                                            SubjectName = p.Subject.Name
                                                        })
                                                        .ToListAsync();
            var classes = await context.Classes.AsNoTracking()
                                               .Include(p => p.School)
                                               .Where(p => p.SchoolId == view.SchoolId)
                                               .Select(p => new
                                               {
                                                   p.Id,
                                                   p.FullId,
                                                   p.Name,
                                                   SchoolName = p.School.Name
                                               })
                                               .ToListAsync();
            var Teachers = await context.Teacher.AsNoTracking()
                                                .Include(p => p.TeacherTitle)
                                                .Include(p => p.Classes)
                                                .Where(p => p.SchoolId == view.SchoolId &&
                                                            p.MainSubjectId == view.SubjectId &&
                                                            (p.RoleId == "TeacherSuper" || p.RoleId == "Teacher" || p.RoleId2 == "TeacherSuper" || p.RoleId2 == "Teacher")
                                                            && p.TeacherTitleId != null)
                                                .OrderBy(p => p.TeacherTitle == null ? 1000 : p.TeacherTitle.Index)
                                                .ThenBy(p => p.Name)
                                                .Select(p => new
                                                {
                                                    p.TeacherTitle,
                                                    p.TeacherTitleId,
                                                    p.Grade,
                                                    p.Name,
                                                    p.IsMainPower,
                                                    Classes = p.Classes.Select(x => new
                                                    {
                                                        x.SubjectId,
                                                        x.ClassId
                                                    }).ToList()
                                                })
                                                .ToListAsync();
            foreach (var teacher in Teachers.Where(p => p.IsMainPower == true && p.TeacherTitleId != 7).ToList())
            {
                var record = new SubjectMapDetailsView();
                if (teacher.TeacherTitle == null) continue;
                if (teacher.Grade == "الإبتدائى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount1_2;
                else if (teacher.Grade == "الإعدادى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount3;
                else if (teacher.Grade == "الثانوى")
                    record.LegalShare = teacher.TeacherTitle.ShareAmount4;
                record.TeacherName = teacher.Name;
                record.Title = teacher.TeacherTitle.Title;
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId))
                {
                    var cl = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (cl == null) continue;
                    record.Classes.Add(cl.FullId);
                }
                data.TotalMainClasses += record.Classes.Count();
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId).ToList())
                {
                    var Class = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (Class == null) continue;
                    var share = Shares.FirstOrDefault(p => p.ClassName == Class.Name);
                    if (share == null) continue;
                    record.CurrentShare += share.ShareAmount;
                }
                record.Result = record.LegalShare - record.CurrentShare;
                data.TMainMap.Add(record);
            }
            data.TotalMainCurrentShare = data.TMainMap.Sum(p => p.CurrentShare);
            data.TotalMainLegalShare = data.TMainMap.Sum(p => p.LegalShare);
            data.TotalMainResult = data.TMainMap.Sum(p => p.Result);
            foreach (var teacher in Teachers.Where(p => p.IsMainPower == false && p.TeacherTitleId != 7).ToList())
            {
                var record = new SubjectMapDetailsView();
                if (teacher.TeacherTitle == null) continue;
                record.TeacherName = teacher.Name;
                record.Title = teacher.TeacherTitle.Title;
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId))
                {
                    var cl = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (cl == null) continue;
                    record.Classes.Add(cl.FullId);
                }
                data.TotalSecandryClasses += record.Classes.Count();
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId).ToList())
                {
                    var Class = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (Class == null) continue;
                    var share = Shares.FirstOrDefault(p => p.ClassName == Class.Name);
                    if (share == null) continue;
                    record.CurrentShare += share.ShareAmount;
                }
                record.LegalShare = record.CurrentShare;
                record.Result = record.LegalShare - record.CurrentShare;
                data.TSecandryMap.Add(record);
            }
            data.TotalSecandryCurrentShare = data.TSecandryMap.Sum(p => p.CurrentShare);
            data.TotalSecandryLegalShare = data.TSecandryMap.Sum(p => p.LegalShare);
            data.TotalSecandryResult = data.TSecandryMap.Sum(p => p.Result);
            foreach (var teacher in Teachers.Where(p => p.TeacherTitleId == 7).ToList())
            {
                var record = new SubjectMapDetailsView();
                if (teacher.TeacherTitle == null) continue;
                record.TeacherName = teacher.Name;
                record.Title = teacher.TeacherTitle.Title;
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId))
                {
                    var cl = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (cl == null) continue;
                    record.Classes.Add(cl.FullId);
                }
                data.TotalFeeClasses += record.Classes.Count();
                foreach (var item in teacher.Classes.Where(p => p.SubjectId == view.SubjectId).ToList())
                {
                    var Class = classes.FirstOrDefault(p => p.Id == item.ClassId);
                    if (Class == null) continue;
                    var share = Shares.FirstOrDefault(p => p.ClassName == Class.Name);
                    if (share == null) continue;
                    record.CurrentShare += share.ShareAmount;
                }
                record.LegalShare = record.CurrentShare;
                record.Result = 0;
                data.TFeeMap.Add(record);
            }
            data.TotalFeeCurrentShare = data.TFeeMap.Sum(p => p.CurrentShare);
            data.TotalFeeLegalShare = data.TFeeMap.Sum(p => p.LegalShare);
            data.TotalFeeResult = data.TFeeMap.Sum(p => p.Result);

            data.TotalLegalShare = data.TotalMainLegalShare + data.TotalSecandryLegalShare + data.TotalFeeLegalShare;
            data.TotalClasses = data.TotalMainClasses + data.TotalSecandryClasses + data.TotalFeeClasses;
            data.TotalCurrentShare = data.TotalMainCurrentShare + data.TotalSecandryCurrentShare + data.TotalFeeCurrentShare;
            data.TotalResult = data.TotalSecandryResult + data.TotalMainResult + data.TotalFeeResult;
            if (data.TotalResult > 0) data.Report = "يوجد فائض في انصبة المدرسين عن الحصص المطلوبة";
            else if (data.TotalResult < 0) data.Report = "يوجد عجز في انصبة المدرسين عن الحصص المطلوبة";
            else data.Report = "لا يوجد عجز او زيادة";
            //var Created = CeratePdf(data, data.ReportName, $"خطة مادة {Shares.First().SubjectName}", classes.First().SchoolName);
            //if (Created)
            return data;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<ClassSharePowerView> ClassShareForSubject(SubjectMapView view)
        {
            if (view == null) throw new Exception("يجب ادخال جميع البيانات");
            var result = new ClassSharePowerView();
            result.ReportName = $"ميزانية {view.SchoolId} {view.SubjectId} 2.pdf";
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }));
            var ClassSubjectShares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification(new ClassSubjectShareParams()
            {
                SubjectId = view.SubjectId
            }));
            var DisClasses = Classes.DistinctBy(p => p.Name).Select(p => p.Name).ToList();
            foreach (var Class in DisClasses)
            {
                var record = new CSharePowerView();
                record.ClassName = Class;
                record.LegalShare = ClassSubjectShares.Where(p => p.ClassName == Class).Sum(p => p.ShareAmount);
                record.ClassNumber = Classes.Where(p => p.Name == Class).Count();
                result.TotalShare += record.LegalShare;
                result.TShare.Add(record);
            }
            var Created = CeratePdf(result.TShare, result.ReportName, $"ميزانية مادة {ClassSubjectShares.First().Subject.Name}", Classes.First().School.Name);
            if (Created)
                return result;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<TitelsShareViewData> TitlesShare(SubjectMapView view)
        {
            if (view == null) throw new Exception("يجب ادخال جميع البيانات");
            if (view.TeacherId == null || view.TeacherId == 0) throw new Exception("يجب ادخال جميع البيانات");
            var Result = new TitelsShareViewData();
            Result.ReportName = $"ميزانية {view.SchoolId} {view.SubjectId}.pdf";
            var Shares = await unitOfWork.Repository<ClassSubjectShare, long>().GetAllAsync(new ClassSubjectShareSpecification());
            var Titles = await unitOfWork.Repository<TeacherTitle, long>().GetAllAsync(new TeacherTitleSpecification());
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.TeacherId.Value));
            var TCs = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                SubjectId = view.SubjectId,
            }));
            TCs = TCs.Where(p => p.Class.SchoolId == view.SchoolId).ToList();
            var Teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = view.SchoolId
            }));
            if (!TCs.Any() || !Teachers.Any()) throw new Exception("لا يوجد تكليف لهذه المادة بعد");
            foreach (var Title in Titles)
            {
                var record = new TitelsShareView();
                record.Title = Title.Title;
                if (teacher.Grade == "الإبتدائى")
                    record.LegalShare = Title.ShareAmount1_2;
                else if (teacher.Grade == "الإعدادى")
                    record.LegalShare = Title.ShareAmount3;
                else if (teacher.Grade == "الثانوى")
                    record.LegalShare = Title.ShareAmount4;
                var STC = TCs.Where(p => p.Teacher.TeacherTitleId == Title.Id).ToList();
                foreach (var Class in STC)
                {
                    var share = Shares.FirstOrDefault(p => p.ClassName == Class.Class.Name && p.SubjectId == view.SubjectId);
                    record.TotalCurrentShare += share != null ? share.ShareAmount : 0;
                }
                var ntc = TCs.Where(p => p.Teacher.TeacherTitleId == Title.Id).DistinctBy(p => p.Teacher).ToList();
                record.TotalTeachers = ntc.Count();
                foreach (var tc in ntc)
                {
                    if (tc.Teacher.Grade == "الإبتدائى")
                        record.TotalLegalShare += Title.ShareAmount1_2;
                    else if (tc.Teacher.Grade == "الإعدادى")
                        record.TotalLegalShare += Title.ShareAmount3;
                    else if (tc.Teacher.Grade == "الثانوى")
                        record.TotalLegalShare += Title.ShareAmount4;
                }
                if (record.TotalTeachers > 0) Result.Data.Add(record);
            }
            var Created = CeratePdf(Result.Data, Result.ReportName, $"ميزانية مادة {TCs.First().Subject.Name}", teacher.School.Name);
            if (Created)
                return Result;
            throw new Exception("خظأ في تحديد البيانات");
        }
    }
}