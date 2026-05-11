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
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public DashboardService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        #region PDF
        private bool CeratePdf(List<WeeklyReportData> data, int Level, string FileName, string header, string SchoolName)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
            var IMFolderName = Path.Combine("wwwroot", "Resources", "Image");
            var IMFilePath = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName, "Mlogo.png");
            var imgbyts = File.ReadAllBytes(IMFilePath);

            //var IMFolderName2 = Path.Combine("wwwroot", "Resources", "Image");
            //var IMFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName2, "school logo.jpg");
            //var imgbyts2 = File.ReadAllBytes(IMFilePath2);
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
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("صفحه ");
                            x.CurrentPageNumber();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        //col.Item().Text(name);
                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(5);
                                c.RelativeColumn(2);
                                if (Level == 1)
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                }
                                else if (Level == 1)
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                }
                                if (Level == 3)
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                }
                                else
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                }
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الاسم").Bold();
                                if (Level == 1)
                                {
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("كراس اداء صفى").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("كراس الواجب").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("كراس النشاط").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("تقييم أسبوعى").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("مهام شفهية").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("مهام مهارية").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("حضور و مواظبة").Bold();
                                }
                                else if (Level == 1)
                                {
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("كراس الواجب").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("كراس النشاط").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("تقييم أسبوعى").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المهام الأدائية").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المواظبة و السلوك").Bold();
                                }
                                else if (Level == 3)
                                {
                                    h.Cell().Element(bordercell).AlignCenter().Text("تقييم أسبوعى").Bold();
                                    h.Cell().Element(bordercell).AlignCenter().Text("الواجبات المنزلية").Bold();
                                    h.Cell().Element(bordercell).AlignCenter().Text("المواظبة و السلوك").Bold();
                                }
                                else
                                {
                                    h.Cell().Element(bordercell).AlignCenter().Text("تقييم أسبوعى").Bold();
                                    h.Cell().Element(bordercell).AlignCenter().Text("الواجبات المنزلية").Bold();
                                }
                                h.Cell().Element(bordercell).AlignCenter().Text("المجموع").Bold();
                            });
                            int index = 1;
                            foreach (var item in data)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text(index.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Name);
                                if (Level == 1)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Classwork.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Homework.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Activity.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Review.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Oral.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Missions.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Behavior.ToString());
                                }
                                else if (Level == 2)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Homework.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Activity.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Review.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Missions.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Behavior.ToString());
                                }
                                else if (Level == 3)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Review.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Homework.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Behavior.ToString());
                                }
                                else
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Review.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(item.Homework.ToString());
                                }
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Total.ToString());
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
                            table.Cell().AlignCenter().Text("معلم ......................");
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        //إعدادي فقط
        private bool CeratePdf(List<AverageReportData> data, string FileName, string header)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
            var IMFolderName = Path.Combine("wwwroot", "Resources", "Image");
            var IMFilePath = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName, "Mlogo.png");
            var imgbyts = File.ReadAllBytes(IMFilePath);
            //var IMFolderName2 = Path.Combine("wwwroot", "Resources", "Image");
            //var IMFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName2, "school logo.jpg");
            //var imgbyts2 = File.ReadAllBytes(IMFilePath2);
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
                                c.RelativeColumn(1);
                                c.RelativeColumn(4);
                                c.RelativeColumn(1);
                            });
                            //table.Cell().Image(imgbyts2).FitArea();
                            table.Cell().Text("");
                            table.Cell().AlignBottom().Text(header).SemiBold().FontSize(18).AlignCenter();
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("صفحه ");
                            x.CurrentPageNumber();
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
                                c.RelativeColumn(5);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                            });
                            table.Header(h =>
                            {
                                h.Cell().Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الاسم").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("تقييم أسبوعى").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("الواجبات المنزلية").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("المواظبة و السلوك").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("المجموع").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("إمتحان الشهر").Bold();
                                h.Cell().Element(bordercell).AlignCenter().Text("المجموع الكلي").Bold();
                            });
                            int index = 1;
                            foreach (var item in data)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text(index.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Name);
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Review.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Homework.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Behavior.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Total.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.Exam.ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(item.AllTotal.ToString());
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
                            table.Cell().AlignCenter().Text("معلم ......................");
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        //إعدادي فقط
        private bool CeratePdf(List<MonthlyReportData> data, string FileName, string header)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
            var IMFolderName = Path.Combine("wwwroot", "Resources", "Image");
            var IMFilePath = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName, "Mlogo.png");
            var imgbyts = File.ReadAllBytes(IMFilePath);
            //var IMFolderName2 = Path.Combine("wwwroot", "Resources", "Image");
            //var IMFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), IMFolderName2, "school logo.jpg");
            //var imgbyts2 = File.ReadAllBytes(IMFilePath2);
            var weekscount = data[0].Weeks.Count;
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
                    page.Size(PageSizes.A3.Landscape());
                    page.Margin(4, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(f => f.FontSize(12));
                    page.Header()
                        .AlignCenter()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(4);
                                c.RelativeColumn(1);
                            });
                            //table.Cell().Image(imgbyts2).FitArea();
                            table.Cell().Text("");
                            table.Cell().AlignBottom().Text(header).SemiBold().FontSize(18).AlignCenter();
                            table.Cell().Image(imgbyts).FitArea();
                        });
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("صفحه ");
                            x.CurrentPageNumber();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        col.Item().Table(table =>
                        {
                            //data
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(1);
                                c.RelativeColumn(4);
                                c.RelativeColumn(1);
                                for (int i = 0; i < weekscount * 4; i++)
                                {
                                    c.RelativeColumn(1);
                                }
                            });
                            table.Header(h =>
                            {
                                h.Cell().RowSpan(2).Element(bordercell).AlignCenter().Text("م").Bold();
                                h.Cell().RowSpan(2).Element(bordercell).AlignCenter().Text("الاسم").Bold();
                                h.Cell().RowSpan(2).Element(bordercell).RotateLeft().AlignCenter().Text("إمتحان الشهر").Bold();
                                for (int i = 0; i < weekscount; i++)
                                {
                                    h.Cell().ColumnSpan(4).Element(bordercell).AlignCenter().Text(data[0].Weeks[i].WeekName).Bold();
                                }
                                for (int i = 0; i < weekscount; i++)
                                {
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("تقييم أسبوعى").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("الواجبات المنزلية").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المواظبة و السلوك").Bold();
                                    h.Cell().Element(bordercell).RotateLeft().AlignCenter().Text("المجموع").Bold();
                                }
                            });
                            for (int i = 0; i < data.Count; i++)
                            {
                                table.Cell().Element(bordercell).AlignCenter().Text((i + 1).ToString());
                                table.Cell().Element(bordercell).AlignCenter().Text(data[i].StudentName);
                                table.Cell().Element(bordercell).AlignCenter().Text(data[i].Exam.ToString());
                                for (int j = 0; j < weekscount; j++)
                                {
                                    table.Cell().Element(bordercell).AlignCenter().Text(data[i].Weeks[j].Review.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(data[i].Weeks[j].Homework.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(data[i].Weeks[j].Behavior.ToString());
                                    table.Cell().Element(bordercell).AlignCenter().Text(data[i].Weeks[j].Total.ToString());
                                }
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
                            table.Cell().AlignCenter().Text("معلم ......................");
                            table.Cell().AlignCenter().Text("مشرف المادة ......................");
                            table.Cell().AlignCenter().Text("مدير المدرسة ......................");
                        });
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        //إعدادي فقط
        private bool CeratePdf(List<StudentMonthlyReport> data, string FileName)
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
                                c.RelativeColumn(1);
                                c.RelativeColumn(4);
                                c.RelativeColumn(1);
                            });
                            table.Cell().Text("");
                            table.Cell().AlignBottom().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                });
                                table.Cell().Text("مديرية التربية والتعليم بالمنوفية");
                                table.Cell().Text("إدارة سرس الليان التعليمية");
                                table.Cell().Text("مدرسة الشهيد هشام قنديل الإعدادية المشتركة");
                            });
                            table.Cell().Image(imgbyts).FitArea();
                            table.Cell().ColumnSpan(3).Text($"تقرير بدرجات الطالب لشهر {data[0].MonthName} للعام الدراسى 2025 - 2026م").FontColor(Colors.Red.Medium).AlignCenter().Bold().FontSize(16);
                        });
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("صفحه ");
                            x.CurrentPageNumber();
                        });
                    page.Content()
                    .Padding(3, Unit.Millimetre)
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Spacing(20);
                        foreach (var student in data)
                        {
                            bool IsIn = true;
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(5);
                                });
                                table.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("اسم الطالب");
                                table.Cell().Element(bordercell).AlignCenter().Text(student.StudentName);
                                table.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("الصف");
                                table.Cell().Element(bordercell).AlignCenter().Text(student.ClassName);
                            });

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(5);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                });
                                table.Header(h =>
                                {
                                    h.Cell().RowSpan(3).Background(Colors.Grey.Lighten3).Element(bordercell).AlignMiddle().AlignCenter().Text("المادة").Bold();
                                    h.Cell().ColumnSpan(6).Background(Colors.Yellow.Medium).Element(bordercell).AlignCenter().Text("الفصل الدراسى الثاني").Bold();
                                    h.Cell().ColumnSpan(6).Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text(student.MonthName).Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("التقييمات").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("الواجبات").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("المواظبة و السلوك").FontSize(10);
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("المجموع").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("الإختبار الشهرى").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).RotateLeft().AlignMiddle().AlignCenter().Text("إجمالي المادة").Bold();
                                    h.Cell().Background(Colors.Grey.Lighten3).Element(bordercell).AlignCenter().Text("الدرجة").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("20").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("10").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("10").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("40").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("15").Bold();
                                    h.Cell().Background(Colors.Yellow.Lighten4).Element(bordercell).AlignCenter().Text("55").Bold();
                                });
                                foreach (var record in student.SubjectData)
                                {
                                    if (record.Status == "Out" && IsIn == true)
                                    {
                                        table.Cell().Background(Colors.LightBlue.Lighten3).Element(bordercell).AlignCenter().Text("الإجمالى");
                                        table.Cell().ColumnSpan(3).Background(Colors.LightBlue.Lighten3).Element(bordercell).AlignCenter().Text("275");
                                        table.Cell().ColumnSpan(3).Background(Colors.LightBlue.Lighten3).Element(bordercell).AlignCenter().Text($"{student.FullTotal}");
                                        table.Cell().ColumnSpan(7).Background(Colors.LightGreen.Medium).Element(bordercell).AlignCenter().Text("مواد خارج المجموع");
                                        IsIn = false;
                                    }
                                    table.Cell().Element(bordercell).AlignCenter().Text(record.SubjectName);
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.Review}");
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.Homework}");
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.Behavior}");
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.Total}");
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.Exam}");
                                    table.Cell().Element(bordercell).AlignCenter().Text($"{record.AllTotal}");
                                }
                                table.Cell().Element(bordercell).AlignCenter().Text("نسبة الحضور").AlignRight();
                                table.Cell().Element(bordercell).AlignCenter().Text($"{student.Absent}%").AlignRight();
                                table.Cell().ColumnSpan(5).Element(bordercell).AlignMiddle().Text("توقيع ولي الامر :").AlignRight();
                            });
                            col.Item().Text("");
                            col.Item().Table(table =>
                            {

                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                    c.RelativeColumn(1);
                                });
                                table.Cell().AlignCenter().Text("مسئول شئون الطلبة و الإمتحانات");
                                table.Cell().AlignCenter().Text("وكيل المدرسة");
                                table.Cell().AlignCenter().Text("مدير المدرسة");
                                table.Cell().AlignCenter().Text("تهاني بركات");
                                table.Cell().AlignCenter().Text("نجاح النشار");
                                table.Cell().AlignCenter().Text("هشام الجندى");
                                table.Cell().AlignCenter().Text("...........");
                                table.Cell().AlignCenter().Text("...........");
                                table.Cell().AlignCenter().Text("...........");
                            });
                            col.Item().PageBreak();
                        }
                    });
                });
            }).GeneratePdf(FilePath);
            return true;
        }
        #endregion

        #region Teacher
        public async Task<FinishClassDashDataView> FinishTeacherClassDashData(FinishTeacherClassDashView view)
        {
            FinishClassDashDataView dataViews = new FinishClassDashDataView();
            var weeks = await unitOfWork.Repository<Week, long>().GetAllAsync(new WeekSpecification());
            dataViews.Weeks = weeks.Where(w => w.Month == view.Month && w.IsActive == true).Select(w => w.Name).ToList();
            var selectedweeks = weeks.Where(w => w.Month == view.Month && w.IsActive == true).ToList();
            /////////////////////////////
            var ClassScores = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification());
            ClassScores = ClassScores.Where(p => p.Week.Month == view.Month).ToList();
            var TeacherClasses = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                TeacherId = view.TeacherId,
                SubjectId = view.SubjectId
            }
            ));
            if (selectedweeks.Count == 0) throw new Exception("لا يوجد اسابيع لهذا الشهر");
            foreach (var teacherClass in TeacherClasses)
            {
                FinishClassDashViewData data = new FinishClassDashViewData();
                data.ClassFullId = teacherClass.Class.FullId;
                data.ClassId = teacherClass.Class.Id;
                data.TeacherName = teacherClass.Teacher.Name;
                foreach (var week in selectedweeks.OrderBy(p => p.Index).ToList())
                {
                    var check = ClassScores.Any(p => p.IsSaved == true && p.ClassId == teacherClass.ClassId && p.SubjectId == teacherClass.SubjectId && p.WeekId == week.Id);
                    data.WeekChecked.Add(new FinishClassDashViewDataWeeks()
                    {
                        Check = check,
                        WeekId = week.Id
                    });
                }
                dataViews.Data.Add(data);
            }
            dataViews.Data = dataViews.Data.OrderBy(p => p.ClassId).ToList();
            return dataViews;
        }
        public async Task<WeeklyReportDataView> GetWeeklyReport(GetWeeklyReportView view)
        {
            if (view == null) throw new Exception("خظأ في تحديد البيانات");
            var data = new WeeklyReportDataView();
            List<WeeklyReportData> x = new List<WeeklyReportData>();
            var TeacherClass = new TeacherClasses();
            if (view.TeacherClassesId == null)
                TeacherClass = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                {
                    SubjectId = view.SubjectId,
                    ClassId = view.ClassId,
                }));
            else
                TeacherClass = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(view.TeacherClassesId.Value));
            if (TeacherClass == null) throw new Exception("خظأ في تحديد البيانات");
            data.Level = HelperFn.GetLevel(TeacherClass.Class.FullId);
            var Students = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification(new StudentScoreDataParams()
            {
                IsSaved = true,
                WeekId = view.WeekId,
                ClassId = TeacherClass.ClassId,
                SubjectId = TeacherClass.SubjectId
            }));
            if (Students.Count() == 0) throw new Exception("لم يتم الرصد بعد");
            foreach (var item in Students)
            {
                data.weeklyReport.Add(new WeeklyReportData()
                {
                    Id = item.Id,
                    Behavior = item.Behavior,
                    Homework = item.Homework,
                    Review = item.Review,
                    Name = item.Student.Name,
                    Activity = item.Activity,
                    Classwork = item.Classwork,
                    Missions = item.Missions,
                    Oral = item.Oral,
                    Total = item.Behavior + item.Homework + item.Review + item.Activity + item.Classwork + item.Missions + item.Oral
                });
            }
            data.weeklyReport.OrderBy(p => p.Name).ToList();
            //////////////////////////PDF//////////////////////
            data.ReportName = $"WeeklyReport_{view.UserId}_{TeacherClass.Class.FullId}_{TeacherClass.Id}.pdf";
            var Created = CeratePdf(data.weeklyReport, data.Level, data.ReportName, $"تقرير {Students.FirstOrDefault().Week.Name} للصف {TeacherClass.Class.FullId} المادة الدراسية {TeacherClass.Subject.Name}", "");
            if (Created)
                return data;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<MonthlyReportDataView> GetMonthlyReport(GetWeeklyReportView view)
        {
            var data = new MonthlyReportDataView();
            List<MonthlyReportData> listdata = new List<MonthlyReportData>();
            if (view.TeacherClassesId == null) throw new Exception("خظأ في تحديد البيانات");
            var TC = await context.TeacherClasses.AsNoTracking().Include(p => p.Class).Include(p => p.Subject)
                                     .FirstOrDefaultAsync(p => p.Id == view.TeacherClassesId ||
                                                              (p.ClassId == view.ClassId && p.SubjectId == view.SubjectId));
            if (TC == null) throw new Exception("خظأ في تحديد البيانات");
            data.Level = HelperFn.GetLevel(TC.Class.FullId);
            var ClassScores = await context.StudentScoreData.Where(p => p.IsSaved &&
                                                                     p.ClassId == TC.ClassId &&
                                                                     p.SubjectId == TC.SubjectId &&
                                                                     p.Week.Month == (int)view.WeekId)
                                                         .AsNoTracking()
                                                         .Select(p => new
                                                         {
                                                             p.StudentId,
                                                             p.WeekId,
                                                             p.Oral,
                                                             p.Missions,
                                                             p.Activity,
                                                             p.Behavior,
                                                             p.Classwork,
                                                             p.Review,
                                                             p.Homework,
                                                         })
                                                         .ToListAsync();
            if (ClassScores == null || ClassScores.Count() == 0) throw new Exception("لا يوجد تقرير لهذا الشهر");
            var Weeks = await context.Week.Where(p => p.Month == view.WeekId && p.IsActive)
                                          .OrderBy(p => p.Index)
                                          .AsNoTracking()
                                          .ToListAsync();
            var students = await context.Student.AsNoTracking().Where(p => p.ClassId == TC.ClassId)
                                                .OrderBy(p => p.Name)
                                                .Select(p => new
                                                {
                                                    p.Id,
                                                    p.Name
                                                })
                                                .ToListAsync();
            if (Weeks.Count() == 0) throw new Exception("لا يوجد تقرير لهذا الشهر");
            var AvgreportData = await GetAverageReport(view);
            foreach (var student in students)
            {
                MonthlyReportData report = new MonthlyReportData();
                double AvgHomework = 0, AvgReview = 0, AvgBehavior = 0, AvgActivity = 0, AvgMissions = 0, AvgOral = 0, AvgClasswork = 0;
                report.StudentName = student.Name;
                var sdata = ClassScores.Where(p => p.StudentId == student.Id);
                foreach (var Week in Weeks)
                {
                    MonthlyReportWeek weekreport = new MonthlyReportWeek();
                    if (sdata == null || sdata.Count() == 0)
                    {
                        weekreport.Behavior = 0;
                        weekreport.Homework = 0;
                        weekreport.Review = 0;
                        weekreport.Activity = 0;
                        weekreport.Missions = 0;
                        weekreport.Oral = 0;
                        weekreport.Classwork = 0;
                        weekreport.WeekName = Week.Name;
                        weekreport.Total = 0;
                        AvgBehavior += 0;
                        AvgHomework += 0;
                        AvgReview += 0;
                        AvgActivity += 0;
                        AvgMissions += 0;
                        AvgOral += 0;
                        AvgClasswork += 0;
                        report.Weeks.Add(weekreport);
                        continue;
                    }
                    var tData = sdata.FirstOrDefault(p => p.WeekId == Week.Id);
                    if (tData != null)
                    {
                        weekreport.Behavior = tData.Behavior;
                        weekreport.Homework = tData.Homework;
                        weekreport.Review = tData.Review;
                        weekreport.Activity = tData.Activity;
                        weekreport.Missions = tData.Missions;
                        weekreport.Oral = tData.Oral;
                        weekreport.Classwork = tData.Classwork;
                        weekreport.WeekName = Week.Name;
                        weekreport.Total = tData.Behavior + tData.Homework + tData.Review + tData.Activity + tData.Missions + tData.Oral + tData.Classwork;
                        AvgBehavior += tData.Behavior;
                        AvgHomework += tData.Homework;
                        AvgReview += tData.Review;
                        AvgActivity += tData.Activity;
                        AvgMissions += tData.Missions;
                        AvgOral += tData.Oral;
                        AvgClasswork += tData.Classwork;
                    }
                    else
                    {
                        weekreport.Behavior = 0;
                        weekreport.Homework = 0;
                        weekreport.Review = 0;
                        weekreport.Activity = 0;
                        weekreport.Missions = 0;
                        weekreport.Oral = 0;
                        weekreport.Classwork = 0;
                        weekreport.WeekName = Week.Name;
                        weekreport.Total = 0;
                        AvgBehavior += 0;
                        AvgHomework += 0;
                        AvgReview += 0;
                        AvgActivity += 0;
                        AvgMissions += 0;
                        AvgOral += 0;
                        AvgClasswork += 0;
                    }
                    report.Weeks.Add(weekreport);
                }
                var studentavdata = AvgreportData.AverageReport.FirstOrDefault(p => p.Id == student.Id);
                report.Weeks.Add(new MonthlyReportWeek()
                {
                    Oral = studentavdata == null ? 0 : studentavdata.Oral,
                    Behavior = studentavdata == null ? 0 : studentavdata.Behavior,
                    Activity = studentavdata == null ? 0 : studentavdata.Activity,
                    Classwork = studentavdata == null ? 0 : studentavdata.Classwork,
                    Homework = studentavdata == null ? 0 : studentavdata.Homework,
                    Review = studentavdata == null ? 0 : studentavdata.Review,
                    Missions = studentavdata == null ? 0 : studentavdata.Missions,
                    Total = studentavdata == null ? 0 : studentavdata.Total,
                    WeekName = "متوسط الدرجات"
                });
                report.Exam = studentavdata == null ? 0 : studentavdata.Exam;
                report.ExamBehavior = studentavdata == null ? 0 : studentavdata.ExamBehavior;
                listdata.Add(report);
            }

            data.MonthlyReport = listdata.OrderBy(p => p.StudentName).ToList();
            data.ReportName = $"MonthlyReport_{view.UserId}_{TC.Id}_{view.WeekId}_{TC.Class.FullId}_{TC.Subject.Name}.pdf";
            var Created = CeratePdf(data.MonthlyReport, data.ReportName, $"تقرير شهر {view.WeekId} للصف {TC.Class.FullId} المادة الدراسية {TC.Subject.Name}");
            if (Created)
                return data;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public async Task<GetAverageReportDataView> GetAverageReport(GetWeeklyReportView view)
        {
            var data = new GetAverageReportDataView();
            if (view.TeacherClassesId == null && (view.ClassId == null || view.SubjectId == null)) throw new Exception("خظأ في تحديد البيانات");
            var TC = await context.TeacherClasses.AsNoTracking().Include(p => p.Class).Include(p => p.Subject)
                                                 .FirstOrDefaultAsync(p => p.Id == view.TeacherClassesId ||
                                                                          (p.ClassId == view.ClassId && p.SubjectId == view.SubjectId));
            if (TC == null) throw new Exception("خظأ في تحديد البيانات");
            var AllSdata = await context.StudentScoreData.Where(p => p.IsSaved &&
                                                                     p.ClassId == TC.ClassId &&
                                                                     p.SubjectId == TC.SubjectId &&
                                                                     p.Week.Month == (int)view.WeekId)
                                                         .AsNoTracking()
                                                         .Select(p => new
                                                         {
                                                             p.StudentId,
                                                             p.WeekId,
                                                             p.Oral,
                                                             p.Missions,
                                                             p.Activity,
                                                             p.Behavior,
                                                             p.Classwork,
                                                             p.Review,
                                                             p.Homework,
                                                         })
                                                         .ToListAsync();
            var Weeks = await context.Week.Where(p => p.Month == view.WeekId && p.IsActive)
                                          .OrderBy(p => p.Index)
                                          .AsNoTracking()
                                          .ToListAsync();
            var students = await context.Student.Where(p => p.ClassId == TC.ClassId)
                                                .OrderBy(p => p.Name)
                                                .Select(p => new
                                                {
                                                    p.Id,
                                                    p.Name
                                                })
                                                .AsNoTracking()
                                                .ToListAsync();
            var Exams = await context.StudentExamScoreData.Where(p => p.IsSaved && p.Month == (int)view.WeekId &&
                                                                      p.SubjectId == TC.SubjectId && p.ClassId == TC.ClassId)
                                                          .Select(p => new
                                                          {
                                                              p.StudentId,
                                                              p.Behavior,
                                                              p.ExamResult
                                                          })
                                                          .AsNoTracking()
                                                          .ToListAsync();
            if (Weeks.Count() == 0) throw new Exception("لا يوجد تقرير لهذا الشهر");
            if (AllSdata.Count() == 0) throw new Exception("لا يوجد تقرير لهذا الشهر");
            var outweeks = await context.OutOfScore.AsNoTracking()
                                                 .Where(p => p.SchoolId == TC.Class.SchoolId &&
                                                             p.SubjectId == TC.SubjectId &&
                                                             p.ClassName == TC.Class.Name)
                                                 .ToListAsync();
            var StudentsAbsent = await context.StudentAbsentCases.AsNoTracking()
                                                                 .Where(p => p.Student.ClassId == TC.ClassId)
                                                                 .ToListAsync();
            var RestWeeks = new List<Week>();
            foreach (var week in Weeks)
            {
                if (outweeks.FirstOrDefault(p => p.WeekId == week.Id) == null) RestWeeks.Add(week);
            }
            data.Level = HelperFn.GetLevel(TC.Class.FullId);
            var records = new List<AverageReportData>();
            foreach (var student in students)
            {
                int ReviewSkip = 0;
                var sd = new StudentDataAvarge()
                {
                    StudentId = student.Id
                };
                var StudentExam = Exams.FirstOrDefault(p => p.StudentId == student.Id);
                foreach (var week in RestWeeks)
                {
                    var StudentScore = AllSdata.FirstOrDefault(p => p.StudentId == student.Id && p.WeekId == week.Id);
                    if (StudentScore == null) continue;
                    var studentAbsent = StudentsAbsent.Where(p => p.StudentId == student.Id).ToList();
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
                records.Add(new AverageReportData()
                {
                    Activity = sd.Activity,
                    AllTotal = sd.TotalRate + (StudentExam == null ? 0 : StudentExam.ExamResult) + (StudentExam == null ? 0 : StudentExam.Behavior),
                    Behavior = sd.Behavior,
                    Classwork = sd.Classwork,
                    Homework = sd.Homework,
                    Missions = sd.Missions,
                    Oral = sd.Oral,
                    Review = sd.Review,
                    Name = student.Name,
                    Id = student.Id,
                    Exam = StudentExam == null ? 0 : StudentExam.ExamResult,
                    Total = sd.TotalRate,
                    ExamBehavior = StudentExam == null ? 0 : StudentExam.Behavior,
                });
            }
            data.AverageReport = records.OrderBy(p => p.Name).ToList();
            /////////////////////PDF///////////////////
            data.ReportName = $"AverageReport_{view.UserId}_{TC.Id}_{view.WeekId}_{TC.Class.FullId}_{TC.Subject.Name}.pdf";
            var Created = CeratePdf(data.AverageReport, data.ReportName, $"تقرير شهر {view.WeekId} للصف {TC.Class.FullId} المادة الدراسية {TC.Subject.Name}");
            if (Created)
                return data;
            throw new Exception("خظأ في تحديد البيانات");
        }
        public Task<GetAllAverageMonthReport> GetAllAverageMonthReportData(GetAllAverageMonthReportView view)
        {
            throw new Exception("قيد التطوير");
        }
        public async Task<StudentMonthlyReportDataView> GetStudentMonthlyReport(GetStudentMonthlyReportView view)
        {
            var Class = await context.Classes.AsNoTracking()
                                             .Include(p => p.Students)
                                             .FirstOrDefaultAsync(p => p.Id == view.ClassId);
            if (Class == null) throw new Exception("خظأ في تحديد البيانات");
            var CerSet = await context.StudentMonthlyCertificateSetting.AsNoTracking()
                                                                       .Include(p => p.Week)
                                                                       .FirstOrDefaultAsync(p => p.SchoolId == Class.SchoolId &&
                                                                                                 p.ClassName == Class.Name &&
                                                                                                 p.Month == view.Month);
            if (CerSet == null) throw new Exception("خظأ في تحديد البيانات");
            var AllTc = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == Class.SchoolId && p.Subject.Status != "Off")
                                               .Include(p => p.Subject)
                                               .ToListAsync();
            var Subjects = AllTc.DistinctBy(p => p.SubjectId).Select(p => p.Subject).ToList();
            var data = new StudentMonthlyReportDataView();
            var GetScores = await context.StudentMonthlyCertificate.AsNoTracking().Where(p => p.ClassId == view.ClassId).ToListAsync();
            var AllAbsent = await context.StudentAbsentData.AsNoTracking()
                                                           .Where(p => p.ClassId == view.ClassId &&
                                                                       p.IsAbsent &&
                                                                       p.ClassNumber == 1 &&
                                                                       p.Week.Index <= CerSet.Week.Index &&
                                                                       p.Week.IsActive)
                                                           .ToListAsync();
            var StudentsAbsent = await context.StudentAbsentCases.AsNoTracking().Where(p => p.Student.ClassId == view.ClassId).ToListAsync();
            foreach (var student in Class.Students.OrderBy(p => p.Name).ToList())
            {
                var StudentAbsent = AllAbsent.Where(p => p.StudentId == student.Id).ToList();
                var StudentCase = StudentsAbsent.Where(p => p.StudentId == student.Id).ToList();
                int days = 0;
                foreach (var Absent in StudentAbsent)
                {
                    if (!StudentCase.Any(p => p.StartDate <= Absent.Date && p.EndDate >= Absent.Date)) days++;
                }
                var record = new StudentMonthlyReport()
                {
                    ClassName = student.Class.Name,
                    ClassId = student.ClassId.Value,
                    MonthName = DateHelper.MonthNameAr(view.Month),
                    StudentName = student.Name,
                    Absent = Math.Round((1 - ((double)days / (double)CerSet.Week.Index / 7)) * 100, 0)
                };
                var StudentScore = GetScores.Where(p => p.StudentId == student.Id).ToList();
                foreach (var Subject in Subjects.OrderBy(p => p.Status).OrderBy(p => p.Index).ToList())
                {
                    var StudentSubjectScore = StudentScore.FirstOrDefault(p => p.ClassId == view.ClassId && p.SubjectId == Subject.Id);
                    if (StudentSubjectScore == null) throw new Exception($"لم يتم رصد الماده {Subject.Name}");
                    var subjectdata = new StudentMonthlyReportSubjectRecord()
                    {
                        Behavior = StudentSubjectScore.Behavior,
                        Homework = StudentSubjectScore.Homework,
                        Review = StudentSubjectScore.Review,
                        Exam = StudentSubjectScore.Exam,
                        Status = Subject.Status,
                        SubjectName = Subject.Name,
                        Total = StudentSubjectScore.Total,
                        AllTotal = StudentSubjectScore.AllTotal
                    };
                    if (Subject.Status == "In") record.FullTotal += StudentSubjectScore.AllTotal;
                    record.FullTotal = Math.Round(record.FullTotal, 1);
                    record.SubjectData.Add(subjectdata);
                }
                data.StudentMonthlyReport.Add(record);
            }
            data.ReportName = $"StudentMonthlyReport_{view.UserId}_{Class.FullId}_{view.Month}.pdf";
            var Created = CeratePdf(data.StudentMonthlyReport, data.ReportName);
            if (Created)
                return data;
            throw new Exception("خظأ في تحديد البيانات");
        }

        #endregion

        #region Maneger
        public async Task<FinishClassDashDataView> FinishClassDashData(FinishClassDashView view)
        {
            FinishClassDashDataView dataViews = new FinishClassDashDataView();
            var weeks = await unitOfWork.Repository<Week, long>().GetAllAsync(new WeekSpecification());
            dataViews.Weeks = weeks.Where(w => w.Month == view.Month && w.IsActive == true).Select(w => w.Name).ToList();
            var ClassScores = await unitOfWork.Repository<StudentScoreData, long>().GetAllAsync(new StudentScoreDataSpecification());
            ClassScores = ClassScores.Where(p => p.Week.Month == view.Month).ToList();
            var selectedweeks = weeks.Where(w => w.Month == view.Month && w.IsActive == true).ToList();
            var TeacherClassess = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                SubjectId = view.SubjectId
            }));
            var TeacherClasses = TeacherClassess.Where(p => p.Class.SchoolId == view.SchoolId).ToList();
            if (selectedweeks.Count == 0) throw new Exception("لا يوجد اسابيع لهذا الشهر");
            if (TeacherClasses.Count == 0) throw new Exception("لا يوجد فصول");
            foreach (var teacherClass in TeacherClasses)
            {
                FinishClassDashViewData data = new FinishClassDashViewData();
                data.ClassId = teacherClass.Class.Id;
                data.ClassFullId = teacherClass.Class.FullId;
                data.TeacherName = teacherClass.Teacher.Name;
                foreach (var week in selectedweeks)
                {
                    var check = ClassScores.Any(p => p.IsSaved == true && p.ClassId == teacherClass.ClassId && p.SubjectId == teacherClass.SubjectId && p.WeekId == week.Id);
                    data.WeekChecked.Add(new FinishClassDashViewDataWeeks()
                    {
                        WeekId = week.Id,
                        Check = check
                    });
                }
                dataViews.Data.Add(data);
            }
            dataViews.Data = dataViews.Data.OrderBy(p => p.ClassId).ToList();
            return dataViews;
        }
        public async Task<FinishClassDashExamDataView> FinishClassDashExamData(FinishClassDashExamView view)
        {
            FinishClassDashExamDataView data = new();
            var Subjects = await context.StudentScoreData.AsNoTracking()
                                                         .DistinctBy(p => p.Subject)
                                                         .Select(p => new
                                                         {
                                                             Id = p.Subject.Id,
                                                             Name = p.Subject.Name
                                                         })
                                                         .ToListAsync();
            var Classes = await context.Classes.Where(p => p.SchoolId == view.SchoolId && p.Name == view.ClassName)
                                              .AsNoTracking()
                                              .OrderBy(p => p.ClassType)
                                              .ThenBy(p => p.Class)
                                              .ThenBy(p => p.ClassNumber)
                                              .Select(p => new
                                              {
                                                  p.Id,
                                                  p.FullId
                                              })
                                              .ToListAsync();
            var Exams = await context.StudentExamScoreData.Where(p => p.Class.SchoolId == view.SchoolId &&
                                                                      p.Month == view.Month &&
                                                                      p.Class.Name == view.ClassName)
                                                          .AsNoTracking()
                                                          .Select(p => new
                                                          {
                                                              p.IsSaved,
                                                              p.IsSeen,
                                                              p.ClassId,
                                                              p.SubjectId,
                                                              p.ExamResult,
                                                              p.Behavior,
                                                              p.StudentId,
                                                              p.Month
                                                          })
                                                          .ToListAsync();
            data.Level = HelperFn.GetLevel(Classes.First().FullId);
            foreach (var subject in Subjects)
            {
                data.Subjects.Add(subject.Name);
            }
            foreach (var Class in Classes)
            {
                var ClassData = new FinishClassDashExamDataSubjects()
                {
                    ClassId = Class.Id,
                    FullId = Class.FullId
                };
                foreach (var subject in Subjects)
                {
                    sbyte c = 0;
                    if (Exams.Any(p => p.ClassId == Class.Id && p.SubjectId == subject.Id && !p.IsSaved))
                        c = -1;
                    else if (Exams.Any(p => p.ClassId == Class.Id && p.SubjectId == subject.Id && !p.IsSeen))
                        c = 0;
                    else
                        c = 1;
                    ClassData.Checks.Add(new FinishClassDashExamDataSubjectsChecks()
                    {
                        SubjectId = subject.Id,
                        Cheak = c
                    });
                }
                data.Classes.Add(ClassData);
            }
            return data;
        }
        public async Task<List<GetVacationView>> GetAdminVacation()
        {
            var vacations = await unitOfWork.Repository<AdministrationsVacation, long>().GetAllAsync(new AdministrationsVacationSpecification());
            if (vacations is null) throw new Exception("لا يوجد اجازات");
            return vacations.Select(p => new GetVacationView()
            {
                Id = p.Id,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Reason = p.Reason
            }).ToList();
        }
        public async Task<List<GetVacationView>> GetSchoolVacation(long SchoolId)
        {
            var vacations = await unitOfWork.Repository<SchoolVacation, long>().GetAllAsync(new SchoolVacationSpecification());
            vacations = vacations.Where(p => p.SchoolId == SchoolId).ToList();
            if (vacations is null) throw new Exception("لا يوجد اجازات");
            return vacations.Select(p => new GetVacationView()
            {
                Id = p.Id,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Reason = p.Reason
            }).ToList();
        }
        public async Task<ErrorResponce> DeleteAdminVacation(long VacationId = 0)
        {
            if (VacationId == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var vacation = await unitOfWork.Repository<AdministrationsVacation, long>().GetByIdAsync(new AdministrationsVacationSpecification(VacationId));
            if (vacation is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            unitOfWork.Repository<AdministrationsVacation, long>().Delete(vacation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الحذف");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteSchoolVacation(long VacationId = 0)
        {
            if (VacationId == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var vacation = await unitOfWork.Repository<SchoolVacation, long>().GetByIdAsync(new SchoolVacationSpecification(VacationId));
            if (vacation is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            unitOfWork.Repository<SchoolVacation, long>().Delete(vacation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الحذف");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddAdminVacation(AddVacationView view)
        {
            if (view.Reason == null || view.StartDate > view.EndDate) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var existingVacation = await unitOfWork.Repository<AdministrationsVacation, long>().GetAllAsync(new AdministrationsVacationSpecification());
            var overlap = existingVacation.Any(p => (view.StartDate >= p.StartDate && view.StartDate <= p.EndDate) || (view.EndDate >= p.StartDate && view.EndDate <= p.EndDate));
            if (overlap) return new ErrorResponce(400, "تاريخ الاجازة يتداخل مع اجازة اخرى");
            var vacation = new AdministrationsVacation()
            {
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                Reason = view.Reason,
                AdministrationId = 1 // Default AdministrationId
            };
            await unitOfWork.Repository<AdministrationsVacation, long>().AddAsync(vacation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddSchoolVacation(AddVacationView view)
        {
            if (view.Reason == null || view.StartDate > view.EndDate || view.SchoolId == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var existingVacation = await unitOfWork.Repository<SchoolVacation, long>().GetAllAsync(new SchoolVacationSpecification(new SchoolVacationParams()
            {
                SchoolId = view.SchoolId
            }));
            var overlap = existingVacation.Any(p => (view.StartDate >= p.StartDate && view.StartDate <= p.EndDate) || (view.EndDate >= p.StartDate && view.EndDate <= p.EndDate));
            if (overlap) return new ErrorResponce(400, "تاريخ الاجازة يتداخل مع اجازة اخرى");
            var vacation = new SchoolVacation()
            {
                StartDate = view.StartDate,
                EndDate = view.EndDate,
                Reason = view.Reason,
                SchoolId = view.SchoolId
            };
            await unitOfWork.Repository<SchoolVacation, long>().AddAsync(vacation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        #endregion
    }
}