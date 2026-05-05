﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.Teacher;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Services.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork unitOfWork;
        public TeacherService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<ErrorResponce> AddTeacher(AddTeacherView view)
        {
            var Teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = view.SchoolId,
                Search = view.Name
            }));
            if (Teacher is not null) return new ErrorResponce(400, "هذا الاسم موجود");
            if (view.RoleId == "TeacherSuper")
            {
                if (view.Grade == null || view.MainSubjectId == null || view.TeacherTitleId == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
                var existingSupervisors = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams()
                {
                    SchoolId = view.SchoolId,
                    SubjectId = view.MainSubjectId,
                    Stage = view.Grade
                }));
                if (existingSupervisors is not null) return new ErrorResponce(400, "يوجد مشرف لهذه المادة و المرحلة بالفعل");
                await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
                {
                    Name = view.Name,
                    Password = "000000",
                    RoleId = view.RoleId,
                    SchoolId = view.SchoolId,
                    Grade = view.Grade,
                    MainSubjectId = view.MainSubjectId,
                    TeacherTitleId = view.TeacherTitleId,
                    IsMainPower = view.IsMainPower,
                });
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    var newSupervisor = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(new TeacherParams()
                    {
                        SchoolId = view.SchoolId,
                        Search = view.Name
                    }));
                    await unitOfWork.Repository<TeacherSupervisor, long>().AddAsync(new TeacherSupervisor()
                    {
                        SchoolId = view.SchoolId,
                        SubjectId = view.MainSubjectId.Value,
                        TeacherId = newSupervisor.Id,
                        Stage = view.Grade
                    });
                    unitOfWork.Repository<Teacher, long>().Update(newSupervisor);
                    var result2 = await unitOfWork.SaveChangesAsync();
                    if (result2 > 0)
                        return new ErrorResponce(200, "تم اضافة المشرف");
                }
            }
            else if (view.RoleId == "Teacher")
            {
                if (view.Grade == null || view.MainSubjectId == null || view.TeacherTitleId == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
                await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
                {
                    Name = view.Name,
                    Password = "000000",
                    RoleId = view.RoleId,
                    SchoolId = view.SchoolId,
                    Grade = view.Grade,
                    MainSubjectId = view.MainSubjectId,
                    TeacherTitleId = view.TeacherTitleId,
                    IsMainPower = view.IsMainPower,
                });
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "تم اضافه المعلم");
            }
            else if (view.RoleId == "guest")
            {
                await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
                {
                    Name = view.Name,
                    Password = "555555",
                    RoleId = view.RoleId,
                    SchoolId = view.SchoolId,
                    IsMainPower = false,
                });
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "تم الإضافة بنجاح");
            }
            else
            {
                await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
                {
                    Name = view.Name,
                    Password = "000000",
                    RoleId = view.RoleId,
                    SchoolId = view.SchoolId,
                    IsMainPower = view.IsMainPower,
                });
                var result = await unitOfWork.SaveChangesAsync();
                if (result > 0) return new ErrorResponce(200, "تم الإضافة بنجاح");
            }
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<string>> GetTeacherSubjects(long TeacherId)
        {
            var Classes = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams() { TeacherId = TeacherId }));
            if (Classes is null) throw new NotImplementedException("هذا المعلم غير موجود");
            return Classes.Select(p => p.Subject.Name).ToList();
        }
        public async Task<List<StudentForClassView>> GetStudentForClass(long ClassId)
        {
            if (ClassId == null) throw new NotImplementedException("ادخل المعلم");
            var Class = await unitOfWork.Repository<Classes, long>().GetByIdAsync(new ClassesSpecification(ClassId));
            if (Class is null) throw new NotImplementedException("هذا الفصل غير موجود");
            return Class.Students.Select(p => new StudentForClassView() { Id = p.Id, Name = p.Name }).ToList();
        }
        public async Task<ErrorResponce> DeleteTeacher(long TeacherId)
        {
            if (TeacherId == null) return new ErrorResponce(400, "ادخل المعلم");
            var Teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(TeacherId));
            if (Teacher is null) return new ErrorResponce(400, "هذا المعلم غير موجود");
            unitOfWork.Repository<Teacher, long>().Delete(Teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف المعلم");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> EditTeacher(EditTeacherView teacher)
        {
            if (teacher == null) return new ErrorResponce(400, "ادخل المعلم");
            var Teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(teacher.Id));
            if (Teacher is null) return new ErrorResponce(400, "هذا المعلم غير موجود");
            Teacher.Name = teacher.Name ?? Teacher.Name;
            Teacher.Password = teacher.Password ?? Teacher.Password;
            Teacher.RoleId = teacher.RoleId ?? Teacher.RoleId;
            Teacher.TeacherTitleId = teacher.TitleId ?? Teacher.TeacherTitleId;
            Teacher.TeacherTitleDate = teacher.TitleDate ?? Teacher.TeacherTitleDate;
            unitOfWork.Repository<Teacher, long>().Update(Teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تعديل بيانات المعلم");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AssignTeacherToClass(TeacherToClassView View)
        {
            if (View == null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var Assign = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                ClassId = View.ClassId,
                SubjectId = View.SubjectId
            }));
            if (Assign is not null) return new ErrorResponce(400, "هذا التعيين موجود مسبقا");
            await unitOfWork.Repository<TeacherClasses, long>().AddAsync(new TeacherClasses()
            {
                ClassId = View.ClassId,
                SubjectId = View.SubjectId,
                TeacherId = View.TeacherId
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه التعيين");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<GetTeachersView>> GetTeachers(long TeacherId)
        {
            var Ttype = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(TeacherId));
            if (Ttype is null) throw new NotImplementedException("هذا المعلم غير موجود");
            if (Ttype.RoleId == "TeacherSuper")
            {
                var teacher = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
                {
                    MainSubjectId = Ttype.MainSubjectId,
                    SchoolId = Ttype.SchoolId
                }));
                if (teacher is null) throw new NotImplementedException("لا يوجد معلمين تابعين لهذا المشرف");
                return teacher.OrderBy(p => p.Name).Select(p => new GetTeachersView()
                {
                    Name = p.Name,
                    Role = p.Role.RoleArabic,
                    Id = p.Id
                }).ToList();
            }
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = Ttype.SchoolId
            }));
            teachers = teachers.Where(p => p.RoleId == "Teacher" || p.RoleId == "TeacherSuper");
            if (teachers is null) throw new NotImplementedException("لا يوجد معلمين في هذه المدرسة");
            var res = teachers.OrderBy(p => p.Name).Select(p => new GetTeachersView()
            {
                Name = p.Name,
                Role = p.Role.RoleArabic,
                Id = p.Id
            }).ToList();
            return res;
            //else throw new NotImplementedException("غير مسموح لك لتحديد معلمين");
        }
        public async Task<ErrorResponce> AddClass(AddClassView view)
        {
            var Class = new Classes();
            var G = "";
            var L = 0;
            if (view.ClassId == null) return new ErrorResponce(400, "ادخل الصف");
            if (view.GradId == null) return new ErrorResponce(400, "ادخل المرحلة");
            if (view.ClassNumber == 0) return new ErrorResponce(400, "ادخل رقم الفصل");
            if (view.SchoolId == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            //Grads: string[] = ['الإبتدائى', 'الإعدادى', 'الثانوى'];
            //Class: string[] = ['الاول', 'الثاني', 'الثالث'];
            Class.Grade = view.GradId;
            if (view.GradId == "الإبتدائى")
                G = "ب";
            else if (view.GradId == "الإعدادى")
                G = "ع";
            else
                G = "ث";

            if (view.ClassId == "الاول")
                L = 1;
            else if (view.ClassId == "الثاني")
                L = 2;
            else if (view.ClassId == "الثالث")
                L = 3;
            else if (view.ClassId == "الرابع")
                L = 4;
            else if (view.ClassId == "الخامس")
                L = 5;
            else if (view.ClassId == "السادس")
                L = 6;
            else
                return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            Class.FullId = $"{L}-{view.ClassNumber} {G}";
            Class.Class = L;
            Class.ClassNumber = view.ClassNumber;
            Class.SchoolId = view.SchoolId;
            Class.Name = $"{view.ClassId} {view.GradId}";
            var classes = await unitOfWork.Repository<Classes, long>().GetByIdAsync(new ClassesSpecification(new ClassesParams()
            {
                FullId = Class.FullId,
                SchoolId = view.SchoolId,
            }));
            if (classes is not null) return new ErrorResponce(400, "هذا الفصل موجود");
            await unitOfWork.Repository<Classes, long>().AddAsync(Class);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه الفصل");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddSubject(string SubjectName)
        {
            if (SubjectName == null) return new ErrorResponce(400, "ادخل اسم الماده الدراسيه");
            var subject = await unitOfWork.Repository<Subject, long>().GetAllAsync(new SubjectSpecification());
            var sub = subject.FirstOrDefault(p => p.Name == SubjectName);
            if (sub is not null) return new ErrorResponce(400, "هذه الماده الدراسيه موجوده");
            await unitOfWork.Repository<Subject, long>().AddAsync(new Subject()
            {
                Name = SubjectName
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه الماده الدراسيه");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<GetAssignTeacherView>> GetAssignTeacher(AssignTeacherView view)
        {
            var classes = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }));
            var sub = await unitOfWork.Repository<Subject, long>().GetByIdAsync(new SubjectSpecification(view.SubjectId));
            var list = new List<GetAssignTeacherView>();
            foreach (var Class in classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
            {
                var Assigns = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                {
                    SubjectId = view.SubjectId,
                    ClassId = Class.Id
                }));
                if (Assigns is null)
                {
                    list.Add(new GetAssignTeacherView()
                    {
                        Id = 0,
                        ClassId = Class.FullId,
                        SubjectName = sub.Name,
                        TeacherName = "لم يتم تعيين معلم بعد",
                    });
                }
                else
                {
                    list.Add(new GetAssignTeacherView()
                    {
                        Id = Assigns.Id,
                        ClassId = Class.FullId,
                        SubjectName = sub.Name,
                        TeacherName = Assigns.Teacher != null ? Assigns.Teacher.Name : "لم يتم تعيين معلم بعد",
                    });
                }

            }
            return list;
        }
        public async Task<List<IdNumberNameView>> GetUnAssignClasses(AssignTeacherView view)
        {
            var classes = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = view.SchoolId
            }));
            var sub = await unitOfWork.Repository<Subject, long>().GetByIdAsync(new SubjectSpecification(view.SubjectId));
            var list = new List<IdNumberNameView>();
            foreach (var Class in classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).ToList())
            {
                var Assigns = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                {
                    SubjectId = view.SubjectId,
                    ClassId = Class.Id
                }));
                if (Assigns is null)
                {
                    list.Add(new IdNumberNameView()
                    {
                        Id = Class.Id,
                        Name = Class.FullId
                    });
                }
                else
                {
                    if (Assigns.Teacher == null)
                    {
                        list.Add(new IdNumberNameView()
                        {
                            Id = Class.Id,
                            Name = Class.FullId,
                        });
                    }
                }
            }
            return list;
        }
        public async Task<ErrorResponce> DeleteAssign(long AssignId)
        {
            var Assign = await unitOfWork.Repository<TeacherClasses, long>().GetByIdAsync(new TeacherClassesSpecification(AssignId));
            if (Assign is null) return new ErrorResponce(400, "هذا التعيين غير موجود");
            unitOfWork.Repository<TeacherClasses, long>().Delete(Assign);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف التعيين");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddPA(AddPAView view)
        {
            if (view.Name == null) return new ErrorResponce(400, "ادخل الاسم");
            var Teacher = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification());
            var sub = Teacher.FirstOrDefault(p => p.Name == view.Name && p.SchoolId == view.SchoolId);
            if (sub is not null && (sub.RoleId == "PA" || sub.RoleId == "SubManager")) return new ErrorResponce(400, "هذا الموظف موجود");
            if (sub is not null && (sub.RoleId != "PA" || sub.RoleId != "SubManager")) return new ErrorResponce(400, "هذا الموظف موجود بوظيفة اخرى");
            await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
            {
                Name = view.Name,
                RoleId = view.RoleId,
                SchoolId = view.SchoolId,
                Password = "000000",
                IsMainPower = true,
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافه ");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<IdNumberNameView>> GetPA(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId
            }));
            teachers = teachers.Where(p => p.RoleId == "PA" || p.RoleId == "SubManager");
            if (teachers is null) throw new NotImplementedException("لا يوجد شؤون عاملين");
            return teachers.OrderBy(p => p.Name).Select(p => new IdNumberNameView()
            {
                Name = p.Name,
                Id = p.Id
            }).ToList();
        }
        public async Task<ErrorResponce> DeletePA(long PAId)
        {
            var Assign = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(PAId));
            if (Assign is null) return new ErrorResponce(400, "هذا العامل غير موجود");
            unitOfWork.Repository<Teacher, long>().Delete(Assign);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم حذف العامل");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> CheckManagement(CheckManagementView view)
        {
            var teachers = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams()
            {
                SchoolId = view.SchoolId,
                SubjectId = view.SubjectId,
                Stage = view.Stage
            }));
            if (teachers is null) return new ErrorResponce(400, "لا يوجد مشرف يجب تعيين مشرف للمادة و المرحلة اولا");
            return new ErrorResponce(200, "");
        }
        public async Task<ErrorResponce> ResetPassword(long TeacherId)
        {
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(TeacherId));
            if (teacher is null) return new ErrorResponce(400, "هذا الموظف غير موجود");
            teacher.Password = "000000";
            unitOfWork.Repository<Teacher, long>().Update(teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم إعادة تعيين كلمة المرور");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<GetTeachersView>> GetAllWorkers(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId
            }));
            var res = teachers.OrderBy(p => p.Role.Index).ThenBy(p => p.TeacherTitle != null ? p.TeacherTitle.Index : 0).ThenBy(p => p.TeacherTitleDate).Select(p => new GetTeachersView()
            {
                Name = p.Name,
                Role = p.Role.RoleArabic,
                Id = p.Id,
                Title = p.TeacherTitle != null ? p.TeacherTitle.Title : ""
            }).ToList();
            return res;
        }
        public async Task<List<IdNumberNameView>> GetSuperSubjects(long TeacherId)
        {
            var result = new List<IdNumberNameView>();
            var Tsubjects = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams() { TeacherId = TeacherId }));
            var Subjects = await unitOfWork.Repository<Subject, long>().GetAllAsync(new SubjectSpecification());
            result.Add(new IdNumberNameView()
            {
                Id = 0,
                Name = Tsubjects.Subject.Name,
            });
            if (Tsubjects.SubSubjects.Count > 0)
                foreach (var item in Tsubjects.SubSubjects)
                {
                    var Sub = Subjects.FirstOrDefault(p => p.Id == item.SubjectId);
                    if (Sub != null)
                        result.Add(new IdNumberNameView()
                        {
                            Id = item.Id,
                            Name = Sub.Name,
                        });
                }
            return result;
        }
        public async Task<ErrorResponce> AddSuperSubjects(AddSuperSubjectsView view)
        {
            if (view == null) return new ErrorResponce(400, "ادخل جميع البيانات");
            var TeacherSubject = await unitOfWork.Repository<TeacherSupervisorSubjects, long>().GetByIdAsync(new TeacherSupervisorSubjectsSpecification(new TeacherSupervisorSubjectsParams()
            {
                SubjectId = view.SubjectId,
                TeacherId = view.TeacherId
            }));
            if (TeacherSubject is not null) return new ErrorResponce(400, "هذه المادة موجودة للمشرف");
            var TS = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams()
            {
                TeacherId = view.TeacherId
            }));
            await unitOfWork.Repository<TeacherSupervisorSubjects, long>().AddAsync(new TeacherSupervisorSubjects()
            {
                SubjectId = view.SubjectId,
                TeacherId = TS.Id
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الاضافة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> DeleteSuperSubjects(long SuperSubjectId)
        {
            if (SuperSubjectId == 0) return new ErrorResponce(400, "لايمكن حذف المادة الأساسية");
            var TeacherSubject = await unitOfWork.Repository<TeacherSupervisorSubjects, long>().GetByIdAsync(new TeacherSupervisorSubjectsSpecification(SuperSubjectId));
            if (TeacherSubject != null)
                unitOfWork.Repository<TeacherSupervisorSubjects, long>().Delete(TeacherSubject);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم الحذف");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<List<IdNumberNameView>> GetSupervisors(long SchoolId)
        {
            var result = new List<IdNumberNameView>();
            var Tsubjects = await unitOfWork.Repository<TeacherSupervisor, long>().GetAllAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams() { SchoolId = SchoolId }));
            return Tsubjects.Select(p => new IdNumberNameView()
            {
                Id = p.Teacher.Id,
                Name = p.Teacher.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> SpearSubjects(long SchoolId)
        {
            var AllSubjects = await unitOfWork.Repository<Subject, long>().GetAllAsync(new SubjectSpecification());
            var listsubject = AllSubjects.ToList();
            var Tsubjects = await unitOfWork.Repository<TeacherSupervisor, long>().GetAllAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams() { SchoolId = SchoolId }));
            var main = Tsubjects.DistinctBy(p => p.SubjectId).Select(p => p.SubjectId);
            foreach (var item in main)
            {
                listsubject.RemoveAll(p => p.Id == item);
            }
            var Ssubjects = await unitOfWork.Repository<TeacherSupervisorSubjects, long>().GetAllAsync(new TeacherSupervisorSubjectsSpecification());
            var sub = Ssubjects.Where(p => p.Teacher.SchoolId == SchoolId).DistinctBy(p => p.SubjectId).Select(p => p.SubjectId);
            foreach (var item in sub)
            {
                listsubject.RemoveAll(p => p.Id == item);
            }
            return listsubject.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetNonSupervisors(long TeacherId)
        {
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(TeacherId));
            if (teacher is null) throw new NotImplementedException("هذا المعلم غير موجود");
            var nonSupervisors = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                MainSubjectId = teacher.MainSubjectId
            }));
            return nonSupervisors.Where(t => t.RoleId == "Teacher")
                .Select(t => new IdNumberNameView()
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList();
        }
        public async Task<ErrorResponce> ChangeSupervisor(ChangeSupervisorView view)
        {
            var oldSupervisor = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.OldTeacherId));
            var NewSupervisor = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.NewTeacherId));
            if (oldSupervisor is null) return new ErrorResponce(400, "المشرف القديم غير موجود");
            var oldTS = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams()
            {
                TeacherId = view.OldTeacherId,
            }));
            oldTS.TeacherId = view.NewTeacherId;
            unitOfWork.Repository<TeacherSupervisor, long>().Update(oldTS);
            oldSupervisor.RoleId = "Teacher";
            NewSupervisor.RoleId = "TeacherSuper";
            unitOfWork.Repository<Teacher, long>().Update(oldSupervisor);
            unitOfWork.Repository<Teacher, long>().Update(NewSupervisor);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تغيير المشرف بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ChangeQualityControl(ChangeQuiltyControlView view)
        {
            var Teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = view.SchoolId
            }));

            foreach (var Teacher in Teachers)
            {
                Teachers.First(p => p.Id == Teacher.Id).HasQuiltyControl = false;
            }
            try
            {
                Teachers.First(p => p.Id == view.TeacherId).HasQuiltyControl = true;
            }
            catch { }
            unitOfWork.Repository<Teacher, long>().UpdateRange(Teachers);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تغيير مراقب جودة التعليم بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<IdStringNameView> GetQualityControlTeacherName(long SchoolId)
        {
            var Teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId,
                HasQuiltyControl = true
            }));
            if (Teacher == null) return new IdStringNameView() { Id = "-", Name = "-" };
            else return new IdStringNameView() { Id = Teacher.Role.RoleArabic, Name = Teacher.Name };
        }
    }
}