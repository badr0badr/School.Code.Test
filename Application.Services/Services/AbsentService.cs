﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Repository;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Services.Services
{
    public class AbsentService : IAbsentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public AbsentService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }

        public async Task<ErrorResponce> IsAbsentClassExist(IsAbsentClassExistView View)
        {
            if (View.TeacherClassesId == null && (View.TeacherId == null || View.ClassId == null)) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdNoTrackingAsync(new TeacherClassesSpecification(View.TeacherClassesId ?? 0, false));
            if (View.TeacherId != null && View.ClassId != null && TC == null)
            {
                TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdNoTrackingAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                {
                    TeacherId = View.TeacherId,
                    ClassId = View.ClassId
                }));
            }
            var existingabsent = await unitOfWork.Repository<StudentAbsentData, long>().GetByIdNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                ClassId = TC != null ? TC.ClassId : View.ClassId,
                ClassNumber = View.ClassNumber
            }));
            if (existingabsent != null)
                return new ErrorResponce(401, "تم تسجيل غياب الصف مسبقأ");
            if (View.IsTwoPart)
            {
                var existingabsent2 = await unitOfWork.Repository<StudentAbsentData, long>().GetByIdNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    ClassId = TC != null ? TC.ClassId : View.ClassId,
                    ClassNumber = View.ClassNumber + 1
                }));
                if (existingabsent2 != null)
                    return new ErrorResponce(401, "تم تسجيل غياب الصف مسبقأ للفترة");
            }
            return new ErrorResponce(200, "");
        }
        public async Task<ErrorResponce> DeleteAbsent(long ClassId)
        {
            var existingabsent = await unitOfWork.Repository<StudentAbsentData, long>().GetAllAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                ClassId = ClassId,
                ClassNumber = 1
            }));
            if (existingabsent != null)
            {
                unitOfWork.Repository<StudentAbsentData, long>().DeleteRange(existingabsent);
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "تم حذف الطالب");
            }
            return new ErrorResponce(401, "خطأ في حذف الغياب");
        }
        public async Task<List<StudentAbsentDataExist>> GetStudentAbsent(long ClassId)
        {
            var result = new List<StudentAbsentDataExist>();
            var existingabsent = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                ClassId = ClassId,
            }));
            if (existingabsent == null || existingabsent.Count() == 0)
            {
                var students = await unitOfWork.Repository<Student, long>().GetAllNoTrackingAsync(new StudentSpecification(new StudentParams()
                {
                    ClassId = ClassId,
                    IsDeleted = false
                }, false));
                foreach (var student in students)
                {
                    result.Add(new StudentAbsentDataExist()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        IsExist = true
                    });
                }
            }
            else
            {
                var LastClassNumber = existingabsent.OrderByDescending(p => p.ClassNumber).First().ClassNumber;
                var data = existingabsent.Where(p => p.ClassNumber == LastClassNumber).OrderBy(p => p.Student.Name).ToList();
                foreach (var student in data)
                {
                    result.Add(new StudentAbsentDataExist()
                    {
                        Id = student.Student.Id,
                        Name = student.Student.Name,
                        IsExist = !student.IsAbsent
                    });
                }
            }
            return result;
        }
        public async Task<ErrorResponce> SaveAbsentStudents(SaveAbsentStudentsView View)
        {
            if (View.Students.Count() == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            if (View.TeacherClassesId == null && (View.TeacherId == null || View.ClassId == null))
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var currentWeek = await unitOfWork.Repository<Week, long>().GetByIdNoTrackingAsync(new WeekSpecification(new WeekParams()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                IsActive = true
            }));
            if (currentWeek == null) return new ErrorResponce(400, "لا يمكن تسجيل غياب لهذا اليوم");
            var CanSave = await IsAbsentClassExist(new IsAbsentClassExistView()
            {
                ClassId = View.ClassId,
                ClassNumber = View.ClassNumber,
                IsTwoPart = View.IsTwoPart,
                TeacherClassesId = View.TeacherClassesId,
                TeacherId = View.TeacherId
            });
            if (CanSave.Status == 200)
            {
                var TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdNoTrackingAsync(new TeacherClassesSpecification(View.TeacherClassesId ?? 0, false));
                if (View.TeacherId != null && View.ClassId != null && TC == null)
                {
                    TC = await unitOfWork.Repository<TeacherClasses, long>().GetByIdNoTrackingAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                    {
                        TeacherId = View.TeacherId,
                        ClassId = View.ClassId
                    }, false));
                }
                foreach (var Score in View.Students)
                {
                    await unitOfWork.Repository<StudentAbsentData, long>().AddAsync(new StudentAbsentData()
                    {
                        StudentId = Score.Id,
                        IsAbsent = !Score.IsExist,
                        Reason = "",
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        ClassNumber = View.ClassNumber,
                        TeacherId = TC != null ? TC.TeacherId : View.TeacherId ?? 0,
                        ClassId = TC != null ? TC.ClassId : View.ClassId ?? 0,
                        SubjectId = TC != null ? TC.SubjectId : null,
                        WeekId = currentWeek.Id
                    });
                }
                if (View.IsTwoPart == true)
                {
                    foreach (var Score in View.Students)
                    {
                        await unitOfWork.Repository<StudentAbsentData, long>().AddAsync(new StudentAbsentData()
                        {
                            StudentId = Score.Id,
                            IsAbsent = !Score.IsExist,
                            Reason = "",
                            Date = DateOnly.FromDateTime(DateTime.Now),
                            ClassNumber = View.ClassNumber + 1,
                            TeacherId = TC != null ? TC.TeacherId : View.TeacherId ?? 0,
                            ClassId = TC != null ? TC.ClassId : View.ClassId ?? 0,
                            SubjectId = TC != null ? TC.SubjectId : null,
                            WeekId = currentWeek.Id
                        });
                    }
                }
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "تم حفظ الطالب");
            }
            return new ErrorResponce(401, "خطأ في حفظ الغياب");
        }
        public async Task<ExamAbsentData> GetExamAbsentData(ExamAbsentDataView view)
        {
            var ExamSetting = await context.StudentMonthlyCertificateSetting.AsNoTracking()
                                                                            .FirstOrDefaultAsync(p => p.SchoolId == view.SchoolId &&
                                                                                                      p.ClassName == view.ClassName &&
                                                                                                      !p.StartCertificates);
            if (ExamSetting == null) throw new Exception("لا يوجد امتحانات لهذا الصف");
            var Teachers = await context.Teacher.AsNoTracking().Where(p => p.SchoolId == view.SchoolId && p.RoleId != "Manager").Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
            var Classes = await context.Classes.AsNoTracking().Where(p => p.SchoolId == view.SchoolId && p.Name == view.ClassName).Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.FullId
            }).ToListAsync();
            var Subjects = await context.TeacherClasses.AsNoTracking()
                                                       .Where(p => p.Class.SchoolId == view.SchoolId &&
                                                                   p.Class.Name == view.ClassName &&
                                                                   p.Subject.Status != "Off")
                                                       .Include(p => p.Subject)
                                                       .Select(p => new IdNumberNameView()
                                                       {
                                                           Id = p.Subject.Id,
                                                           Name = p.Subject.Name
                                                       }).ToListAsync();
            var ExistData = await context.StudentExamScoreData.AsNoTracking()
                                                              .Where(p => p.Class.SchoolId == view.SchoolId &&
                                                                   p.Class.Name == view.ClassName &&
                                                                   p.Month == ExamSetting.Month)
                                                              .Select(p => new
                                                              {
                                                                  p.ClassId,
                                                                  p.SubjectId,
                                                                  p.StudentId
                                                              })
                                                              .ToListAsync();
            var ClassesData = new List<ExamAbsentDataClass>();
            foreach (var Class in Classes)
            {
                var SubjectsData = new List<IdNumberNameView>();
                foreach (var Subject in Subjects.DistinctBy(p => p.Id).ToList())
                {
                    if (!ExistData.Any(p => p.ClassId == Class.Id && p.SubjectId == Subject.Id))
                    {
                        SubjectsData.Add(Subject);
                    }
                }
                if (SubjectsData.Count() > 0)
                {
                    ClassesData.Add(new ExamAbsentDataClass()
                    {
                        Id = Class.Id,
                        Name = Class.Name,
                        Subjects = SubjectsData
                    });
                }
            }
            if (ClassesData.Count() == 0) throw new Exception("لا يوجد امتحانات لهذا الصف");
            return new ExamAbsentData()
            {
                Month = ExamSetting.Month,
                MonthName = DateHelper.MonthNameAr(ExamSetting.Month),
                Teachers = Teachers,
                Classes = ClassesData
            };
        }
        public async Task<List<StudentAbsentDataExist>> GetStudentExamAbsent(long ClassId)
        {
            var result = new List<StudentAbsentDataExist>();
            var students = await unitOfWork.Repository<Student, long>().GetAllNoTrackingAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = ClassId,
                IsDeleted = false
            }, false));
            foreach (var student in students)
            {
                result.Add(new StudentAbsentDataExist()
                {
                    Id = student.Id,
                    Name = student.Name,
                    IsExist = true
                });
            }
            return result;
        }
        public async Task<ErrorResponce> SaveAbsentStudentsExam(SaveAbsentStudentsExamView View)
        {
            if (View.Students.Count() == 0)
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            foreach (var student in View.Students)
            {
                await unitOfWork.Repository<StudentExamScoreData, long>().AddAsync(new StudentExamScoreData()
                {
                    StudentId = student.Id,
                    Behavior = 0,
                    ClassId = View.ClassId,
                    ExamResult = 0,
                    IsAbsent = !student.IsExist,
                    Month = View.Month,
                    SubjectId = View.SubjectId,
                    TeacherId = View.TeacherId
                });
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حفظ الطالب");
            return new ErrorResponce(401, "خطأ في حفظ الغياب");
        }
        public async Task<MainAllAbsentView> GetAllAbsentView(long SchoolId)
        {
            var retultData = new MainAllAbsentView();
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }, false));
            var Abs = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                ClassNumber = 1
            }));
            var H1 = new TopTable() { Name = "الاول الثانوى" };
            var H2 = new TopTable() { Name = "الثاني الثانوى" };
            var G1 = new TopTable() { Name = "الاول الإعدادى" };
            var G2 = new TopTable() { Name = "الثاني الإعدادى" };
            var F1 = new TopTable() { Name = "الاول الإبتدائى" }; ;
            var F2 = new TopTable() { Name = "الثاني الإبتدائى" };
            var F3 = new TopTable() { Name = "الثالث الإبتدائى" };
            var F4 = new TopTable() { Name = "الرابع الإبتدائى" };
            var F5 = new TopTable() { Name = "الخامس الإبتدائى" };
            var F6 = new TopTable() { Name = "السادس الإبتدائى" };
            Abs = Abs.Where(p => p.Class.SchoolId == SchoolId).ToList();
            foreach (var Class in Classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
            {
                var students = await unitOfWork.Repository<Student, long>().GetAllNoTrackingAsync(new StudentSpecification(new StudentParams()
                {
                    ClassId = Class.Id
                }, false));
                var exist = Abs.Where(p => p.ClassId == Class.Id);
                if (exist == null)
                {
                    retultData.Data.Add(new AllAbsentView()
                    {
                        Id = 0,
                        ClassId = Class.Id,
                        ClassFullId = Class.FullId,
                        TeacherName = "",
                        Absent = "لم يتم التسجيل بعد"
                    });
                }
                else if (exist.FirstOrDefault() == null)
                {
                    retultData.Data.Add(new AllAbsentView()
                    {
                        Id = 0,
                        ClassId = Class.Id,
                        ClassFullId = Class.FullId,
                        TeacherName = "",
                        Absent = "لم يتم التسجيل بعد"
                    });
                }
                else if (exist.Where(p => p.IsAbsent == true).Count() == 0)
                {
                    retultData.Data.Add(new AllAbsentView()
                    {
                        Id = exist.FirstOrDefault().Id,
                        ClassId = Class.Id,
                        ClassFullId = Class.FullId,
                        TeacherName = exist.FirstOrDefault().Teacher.Name,
                        Absent = "لا يوجد غائبين"
                    });
                }
                else if (exist.Where(p => p.IsAbsent == true).Count() > 0)
                {
                    retultData.Data.Add(new AllAbsentView()
                    {
                        Id = exist.FirstOrDefault().Id,
                        ClassId = Class.Id,
                        ClassFullId = Class.FullId,
                        TeacherName = exist.FirstOrDefault().Teacher.Name,
                        Absent = exist.Where(p => p.IsAbsent == true).Count().ToString()
                    });
                }

                if (Class.Name == "الاول الثانوى")
                {
                    H1.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    H1.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    H1.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الثاني الثانوى")
                {
                    H2.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    H2.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    H2.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الاول الإبتدائى")
                {
                    F1.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F1.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F1.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الثاني الإبتدائى")
                {
                    F2.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F2.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F2.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الثالث الإبتدائى")
                {
                    F3.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F3.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F3.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الرابع الإبتدائى")
                {
                    F4.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F4.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F4.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الخامس الإبتدائى")
                {
                    F5.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F5.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F5.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "السادس الإبتدائى")
                {
                    F6.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    F6.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    F6.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الاول الإعدادى")
                {
                    G1.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    G1.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    G1.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
                else if (Class.Name == "الثاني الإعدادى")
                {
                    G2.TotalStudent += students.Where(p => p.ClassId == Class.Id).Count();
                    G2.TotalExist += exist != null ? exist.Where(p => p.IsAbsent == true).Count() : 0;
                    G2.TotalAbsent += exist != null ? exist.Where(p => p.IsAbsent == false).Count() : 0;
                }
            }
            H1.Present = H1.TotalStudent > 0 ? (((double)H1.TotalAbsent / (double)H1.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            H2.Present = H2.TotalStudent > 0 ? (((double)H2.TotalAbsent / (double)H2.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F1.Present = F1.TotalStudent > 0 ? (((double)F1.TotalAbsent / (double)F1.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F2.Present = F2.TotalStudent > 0 ? (((double)F2.TotalAbsent / (double)F2.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F3.Present = F3.TotalStudent > 0 ? (((double)F3.TotalAbsent / (double)F3.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F4.Present = F4.TotalStudent > 0 ? (((double)F4.TotalAbsent / (double)F4.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F5.Present = F5.TotalStudent > 0 ? (((double)F5.TotalAbsent / (double)F5.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            F6.Present = F6.TotalStudent > 0 ? (((double)F6.TotalAbsent / (double)F6.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            G1.Present = G1.TotalStudent > 0 ? (((double)G1.TotalAbsent / (double)G1.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            G2.Present = G2.TotalStudent > 0 ? (((double)G2.TotalAbsent / (double)G2.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            var totalsG = new TopTable() { Name = "إجمالي" };
            totalsG.TotalStudent = H1.TotalStudent + H2.TotalStudent;
            totalsG.TotalAbsent = H1.TotalAbsent + H2.TotalAbsent;
            totalsG.TotalExist = H1.TotalExist + H2.TotalExist;
            totalsG.Present = totalsG.TotalStudent > 0 ? (((double)totalsG.TotalAbsent / (double)totalsG.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            retultData.H.Add(H1);
            retultData.H.Add(H2);
            retultData.H.Add(totalsG);

            var totalsF = new TopTable() { Name = "إجمالي" };
            totalsF.TotalStudent = F1.TotalStudent + F2.TotalStudent + F3.TotalStudent + F4.TotalStudent + F5.TotalStudent + F6.TotalStudent;
            totalsF.TotalAbsent = F1.TotalAbsent + F2.TotalAbsent + F3.TotalAbsent + F4.TotalAbsent + F5.TotalAbsent + F6.TotalAbsent;
            totalsF.TotalExist = F1.TotalExist + F2.TotalExist + F3.TotalExist + F4.TotalExist + F5.TotalExist + F6.TotalExist;
            totalsF.Present = totalsF.TotalStudent > 0 ? (((double)totalsF.TotalAbsent / (double)totalsF.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            retultData.F.Add(F1);
            retultData.F.Add(F2);
            retultData.F.Add(F3);
            retultData.F.Add(F4);
            retultData.F.Add(F5);
            retultData.F.Add(F6);
            retultData.F.Add(totalsF);

            var totalsH = new TopTable() { Name = "إجمالي" };
            totalsH.TotalStudent = G1.TotalStudent + G2.TotalStudent;
            totalsH.TotalAbsent = G1.TotalAbsent + G2.TotalAbsent;
            totalsH.TotalExist = G1.TotalExist + G2.TotalExist;
            totalsH.Present = totalsH.TotalStudent > 0 ? (((double)totalsH.TotalAbsent / (double)totalsH.TotalStudent) * 100).ToString("0.00") + "%" : "0%";
            retultData.G.Add(G1);
            retultData.G.Add(G2);
            retultData.G.Add(totalsH);
            return retultData;
        }
        public async Task<List<GetAllAbsentDataView>> GetAllAbsentViewData(GetAllAbsentViewDataView view)
        {
            List<GetAllAbsentDataView> AbsView = new List<GetAllAbsentDataView>();
            var StartClass = 1;
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }, false));
            var Abs = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = view.Date.HasValue ? view.Date.Value : DateOnly.FromDateTime(DateTime.Now),
                ClassNumber = StartClass
            }));
            for (int i = 1; i <= 9; i++)
            {
                GetAllAbsentDataView Absrecord = new GetAllAbsentDataView();
                Absrecord.LessonNumber = StartClass.ToString();
                foreach (var Class in Classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
                {
                    var exist = Abs.Where(p => p.ClassId == Class.Id);
                    if (exist == null)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = 0,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = "لم يتم تسجيل الغياب",
                            Absent = ""
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.FirstOrDefault() == null)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = 0,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = "لم يتم تسجيل الغياب",
                            Absent = ""
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.Where(p => p.IsAbsent == true).Count() == 0)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = exist.FirstOrDefault().Id,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = exist.FirstOrDefault().Teacher.Name,
                            Absent = "0"
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.Where(p => p.IsAbsent == true).Count() > 0)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = exist.FirstOrDefault().Id,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = exist.FirstOrDefault().Teacher.Name,
                            Absent = exist.Where(p => p.IsAbsent == true).Count().ToString()
                        };
                        Absrecord.Data.Add(New);
                    }
                }
                AbsView.Add(Absrecord);
                StartClass += 1;
            }
            return AbsView;
        }
        public async Task<List<GetAllAbsentDataView>> GetTeacherAbsentViewData(GetAllAbsentViewDataView view)
        {
            List<GetAllAbsentDataView> AbsView = new List<GetAllAbsentDataView>();
            var StartClass = 1;
            var NowDate = DateOnly.FromDateTime(DateTime.Now);
            var DayName = DateHelper.GetDayName(DateTime.Now);
            var Classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }, false));
            var General = await unitOfWork.Repository<GeneralSupervision, long>().GetAllNoTrackingAsync(new GeneralSupervisionSpecification(false));
            var GeneralExaist = General.FirstOrDefault(p => p.TeacherId == view.TeacherId && p.SchoolId == view.SchoolId && p.Day == DateHelper.DayNameAr(DayName));
            if (GeneralExaist == null)
            {
                var dayly = await unitOfWork.Repository<DailySupervision, long>().GetAllAsync(new DailySupervisionSpecification());
                if (DayName == "Saturday")
                {
                    Classes = dayly.Where(p => p.Saturday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
                else if (DayName == "Sunday")
                {
                    Classes = dayly.Where(p => p.Sunday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
                else if (DayName == "Monday")
                {
                    Classes = dayly.Where(p => p.Monday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
                else if (DayName == "Tuesday")
                {
                    Classes = dayly.Where(p => p.Tuseday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
                else if (DayName == "Wednesday")
                {
                    Classes = dayly.Where(p => p.Wednesday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
                else if (DayName == "Thursday")
                {
                    Classes = dayly.Where(p => p.Thursday == view.TeacherId && p.SchoolId == view.SchoolId).Select(p => p.Class).ToList();
                }
            }
            for (int i = 1; i <= 9; i++)
            {
                GetAllAbsentDataView Absrecord = new GetAllAbsentDataView();
                Absrecord.LessonNumber = StartClass.ToString();
                var Abs = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
                {
                    Date = view.Date.HasValue ? view.Date.Value : NowDate,
                    ClassNumber = StartClass
                }));
                foreach (var Class in Classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
                {
                    var exist = Abs.Where(p => p.ClassId == Class.Id);
                    if (exist == null)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = 0,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = "لم يتم تسجيل الغياب",
                            Absent = ""
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.FirstOrDefault() == null)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = 0,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = "لم يتم تسجيل الغياب",
                            Absent = ""
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.Where(p => p.IsAbsent == true).Count() == 0)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = exist.FirstOrDefault().Id,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = exist.FirstOrDefault().Teacher.Name,
                            Absent = "0"
                        };
                        Absrecord.Data.Add(New);
                    }
                    else if (exist.Where(p => p.IsAbsent == true).Count() > 0)
                    {
                        var New = new AllAbsentView()
                        {
                            Id = exist.FirstOrDefault().Id,
                            ClassId = Class.Id,
                            ClassFullId = Class.FullId,
                            TeacherName = exist.FirstOrDefault().Teacher.Name,
                            Absent = exist.Where(p => p.IsAbsent == true).Count().ToString()
                        };
                        Absrecord.Data.Add(New);
                    }
                }
                AbsView.Add(Absrecord);
                StartClass += 1;
            }
            return AbsView;
        }
        public async Task<List<AbsentStudentView>> GetAbsentViewById(long Id)
        {
            var FirstStudent = await unitOfWork.Repository<StudentAbsentData, long>().GetByIdNoTrackingAsync(new StudentAbsentDataSpecification(Id, false));
            if (FirstStudent is null) throw new NotImplementedException("لم يتم تسجيل الغياب");
            var absentData = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                Date = FirstStudent.Date,
                ClassId = FirstStudent.ClassId,
                SubjectId = FirstStudent.SubjectId,
                TeacherId = FirstStudent.TeacherId,
                ClassNumber = FirstStudent.ClassNumber
            }));
            if (absentData.Where(p => p.IsAbsent == true).Count() == 0)
                throw new NotImplementedException("لا يوجد غائبين");
            var abs = new List<AbsentStudentView>();
            foreach (var item in absentData.Where(p => p.IsAbsent == true))
            {
                var stepAbs = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
                {
                    StudentId = item.StudentId
                }, false));
                var count = stepAbs.Where(p => p.IsAbsent == true).Count();
                int L = 1;
                if (count >= 7) L = 0;
                if (count >= 10) L = -1;
                abs.Add(new AbsentStudentView()
                {
                    Id = item.Id,
                    StudentName = item.Student.Name,
                    IsExist = !item.IsAbsent,
                    TotalAbsent = count,
                    Level = L
                });
            }
            return abs.OrderBy(p => p.StudentName).ToList(); ;
        }
        public async Task<ErrorResponce> SaveResetAbsentData(List<AbsentStudentView> view)
        {
            if (view.Count() == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            if (view.Where(p => p.IsExist == true).Count() == 0) return new ErrorResponce(200, "تم الحفظ بنجاح");
            foreach (var item in view)
            {
                if (item.IsExist == true)
                {
                    var Abs = await unitOfWork.Repository<StudentAbsentData, long>().GetByIdAsync(new StudentAbsentDataSpecification(item.Id, false));
                    Abs.IsAbsent = false;
                    unitOfWork.Repository<StudentAbsentData, long>().Update(Abs);
                }
            }
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0)
                return new ErrorResponce(200, "تم الحفظ بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<Get5BehaviorView> Get5Behavior(BehaviorView view)
        {
            var Result = new Get5BehaviorView();
            double days = DateTime.DaysInMonth(view.Year, (int)view.Month);
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdNoTrackingAsync(new ClassesSpecification(view.ClassId, false));
            var Studentabsentcases = await unitOfWork.Repository<StudentAbsentCases, long>().GetAllNoTrackingAsync(new StudentAbsentCasesSpecification(false));
            Result.Month = view.Month;
            Result.ClassId = view.ClassId;
            Result.ClassFullId = Class.FullId;
            var Students = await unitOfWork.Repository<Student, long>().GetAllNoTrackingAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = view.ClassId
            }, false));
            var Adminvacations = await unitOfWork.Repository<AdministrationsVacation, long>().GetAllNoTrackingAsync(new AdministrationsVacationSpecification(false));
            var Schoolvacations = await unitOfWork.Repository<SchoolVacation, long>().GetAllNoTrackingAsync(new SchoolVacationSpecification(false));
            Schoolvacations = Schoolvacations.Where(p => p.SchoolId == Class.SchoolId).ToList();
            var Absents = await unitOfWork.Repository<StudentAbsentData, long>().GetAllNoTrackingAsync(new StudentAbsentDataSpecification(new StudentAbsentDataParams()
            {
                ClassId = view.ClassId,
                ClassNumber = 1,
            }, false));
            var Absent = Absents.Where(p => p.Date.Year == view.Year && p.Date.Month == view.Month).ToList();
            foreach (var student in Students)
            {
                var TotalAbs = 0;
                var STrecord = new Student5BehaviorView();
                STrecord.StudentName = student.Name;
                STrecord.StudentId = student.Id;
                var startday = DateOnly.FromDateTime(new DateTime(view.Year, (int)view.Month, 1));
                for (int i = 1; i <= days; i++)
                {
                    var dayrecord = new StudentDayDetails();
                    dayrecord.Day = i;
                    dayrecord.Name = DateHelper.DayNameAr(startday.DayOfWeek.ToString());
                    var studentCase = Studentabsentcases.FirstOrDefault(p => p.StudentId == student.Id && p.StartDate <= startday && p.EndDate >= startday);
                    var dayadminvacation = Adminvacations.FirstOrDefault(p => p.StartDate <= startday && p.EndDate >= startday);
                    var dayschoolvacation = Schoolvacations.FirstOrDefault(p => p.StartDate <= startday && p.EndDate >= startday);
                    if (dayadminvacation != null)
                    {
                        dayrecord.IsExist = 4; // (-) bg-Yellow
                        dayrecord.Reason = dayadminvacation.Reason; ;
                    }
                    else if (dayschoolvacation != null)
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
                        var StudentAbsentstep = Absent.Where(p => p.Date.Day == startday.Day).ToList();
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
                            dayrecord.IsExist = 5; // (-) bg-White
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
        }
    }
}