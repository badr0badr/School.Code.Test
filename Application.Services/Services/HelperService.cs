﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Helper;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.Teacher;
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
    public class HelperService : IHelperService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public HelperService(IUnitOfWork _unitOfWork, AppDbContext _context)
        {
            unitOfWork = _unitOfWork;
            context = _context;
        }
        public async Task<List<IdNumberNameView>> GetSubjects(long? TeacherId)
        {
            if (TeacherId == null || TeacherId == 0)
            {
                var Subject = await unitOfWork.Repository<Subject, long>().GetAllNoTrackingAsync(new SubjectSpecification(false));
                if (Subject is null) throw new Exception("لا يوجد مواد دراسيه");
                return Subject.Select(p => new IdNumberNameView()
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList();
            }
            else
            {
                var TeacherClasses = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
                {
                    TeacherId = TeacherId
                }));
                List<IdNumberNameView> SubjectView = new List<IdNumberNameView>();
                foreach (var teacherClass in TeacherClasses.DistinctBy(p => p.SubjectId).ToList())
                {
                    SubjectView.Add(new IdNumberNameView()
                    {
                        Id = teacherClass.Subject.Id,
                        Name = teacherClass.Subject.Name
                    });
                }
                return SubjectView.OrderByDescending(p => p.Name).ToList();
            }
        }
        public async Task<List<IdNumberNameView>> GetSubjectsInSchool(long SchoolId)
        {
            var AllTc = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == SchoolId)
                                               .Include(p => p.Subject)
                                               .ToListAsync();
            var Subjects = AllTc.DistinctBy(p => p.SubjectId).Select(p => p.Subject).OrderBy(p => p.Index).ToList();
            return Subjects.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetSubjectsInSchoolHasApplied(long SchoolId)
        {
            var Subjects = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == SchoolId && p.Subject.HasAppliedExam)
                                               .Include(p => p.Subject)
                                               .Select(p => p.Subject)
                                               .Distinct()
                                               .ToListAsync();
            return Subjects.OrderBy(p => p.Index).Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetSubjectsInSchoolWithType(IdNumberNameView view)
        {
            var AllTc = await context.TeacherClasses.AsNoTracking()
                                               .Where(p => p.Class.SchoolId == view.Id && p.Subject.Status.ToLower() == view.Name.ToLower())
                                               .Include(p => p.Subject)
                                               .ToListAsync();
            var Subjects = AllTc.DistinctBy(p => p.SubjectId).Select(p => p.Subject).OrderBy(p => p.Index).ToList();
            return Subjects.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetSubjectsSupervisior(long TeacherId)
        {
            var Subjects = new List<IdNumberNameView>();
            var teacher = await unitOfWork.Repository<TeacherSupervisor, long>().GetByIdNoTrackingAsync(new TeacherSupervisorSpecification(new TeacherSupervisorParams()
            {
                TeacherId = TeacherId
            }));
            var sub = await unitOfWork.Repository<TeacherSupervisorSubjects, long>().GetAllNoTrackingAsync(new TeacherSupervisorSubjectsSpecification(new TeacherSupervisorSubjectsParams()
            {
                TeacherId = teacher.Id
            }));
            Subjects.Add(new IdNumberNameView()
            {
                Id = teacher.Subject.Id,
                Name = teacher.Subject.Name
            });
            foreach (var item in sub)
            {
                Subjects.Add(new IdNumberNameView()
                {
                    Id = item.Subject.Id,
                    Name = item.Subject.Name
                });
            }
            return Subjects;
        }
        public async Task<List<IdNumberNameView>> GetClasses(long SchoolId)
        {
            var classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }, false));
            if (classes is null) throw new Exception("لا يوجد فصول");
            return classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.FullId
            }).ToList();
        }
        public async Task<List<string>> GetClassesNames(long SchoolId)
        {
            var classes = await unitOfWork.Repository<Classes, long>().GetAllNoTrackingAsync(new ClassesSpecification(new ClassesParams()
            {
                SchoolId = SchoolId
            }, false));
            if (classes is null) throw new Exception("لا يوجد فصول");
            return classes.OrderBy(p => p.ClassType).ThenBy(p => p.Class).ThenBy(p => p.ClassNumber).DistinctBy(p => p.Name).Select(p => p.Name).ToList();
        }
        public async Task<List<IdNumberNameView>> GetStudent(long ClassId)
        {
            var Students = await unitOfWork.Repository<Student, long>().GetAllNoTrackingAsync(new StudentSpecification(new StudentParams()
            {
                ClassId = ClassId
            }, false));
            if (Students is null) throw new Exception("لا يوجد طلاب");
            return Students.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetWeeks(int? month)
        {
            if (month == null) throw new Exception("يجب اختيار شهر");
            if (month <= 0 && month >= 14) throw new Exception("يجب اختيار شهر");
            var weeks = await unitOfWork.Repository<Week, long>().GetAllNoTrackingAsync(new WeekSpecification());
            var Cweeks = weeks.Where(p => p.IsActive == true);
            if (month > 0 && month < 13)
                Cweeks = Cweeks.Where(weeks => weeks.Month == month);
            List<IdNumberNameView> SubjectView = new List<IdNumberNameView>();
            foreach (var Subject in Cweeks.OrderBy(p => p.StartDate))
            {
                SubjectView.Add(new IdNumberNameView()
                {
                    Id = Subject.Id,
                    Name = Subject.Name
                });
            }
            return SubjectView.ToList();
        }
        public async Task<List<GetTeacherClassesView>> GetTeacherClasses(long TeacherId)
        {
            if (TeacherId == 0) throw new Exception("هذا المعلم غير موجود");
            var TeacherClassesIDs = await unitOfWork.Repository<TeacherClasses, long>().GetAllNoTrackingAsync(new TeacherClassesSpecification(new TeacherClassesParams() { TeacherId = TeacherId }));
            if (TeacherClassesIDs is null) throw new Exception("هذا المعلم غير موجود");
            List<GetTeacherClassesView> gets = new List<GetTeacherClassesView>();
            foreach (var item in TeacherClassesIDs)
            {
                int l = HelperFn.GetLevel(item.Class.FullId);
                gets.Add(new GetTeacherClassesView()
                {
                    Id = item.Id,
                    ClassId = item.ClassId,
                    SubjectId = item.SubjectId,
                    SubjectName = item.Subject.Name,
                    ClassFullId = item.Class.FullId,
                    Level = l
                });
            }
            return gets.OrderBy(p => p.ClassId).ThenBy(p => p.SubjectName).ToList();
        }
        public async Task<List<IdStringNameView>> GetRolesForSchool()
        {
            var role = await unitOfWork.Repository<Role, string>().GetAllNoTrackingAsync(new RoleSpecification());
            if (role is null) throw new Exception("لا يوجد وظائف");
            return role.Where(p => p.SchoolSelection == true).Select(p => new IdStringNameView()
            {
                Id = p.Id,
                Name = p.RoleArabic
            }).ToList();
        }
        public async Task<List<IdStringNameView>> GetRolesForDirectorate()
        {
            var role = await unitOfWork.Repository<Role, string>().GetAllNoTrackingAsync(new RoleSpecification());
            if (role is null) throw new Exception("لا يوجد وظائف");
            return role.Where(p => p.DirectorSelection == true).Select(p => new IdStringNameView()
            {
                Id = p.Id,
                Name = p.RoleArabic
            }).ToList();
        }
        public async Task<List<IdStringNameView>> GetRolesForAdmin()
        {
            var role = await unitOfWork.Repository<Role, string>().GetAllNoTrackingAsync(new RoleSpecification());
            if (role is null) throw new Exception("لا يوجد وظائف");
            return role.Where(p => p.AdminSelection == true).Select(p => new IdStringNameView()
            {
                Id = p.Id,
                Name = p.RoleArabic
            }).ToList();
        }
        public async Task<List<GetSchoolView>> GetSchools()
        {
            var result = new List<GetSchoolView>();
            var schools = await unitOfWork.Repository<School, long>().GetAllNoTrackingAsync(new SchoolSpecification());
            foreach (var school in schools.OrderBy(p => p.index).ThenBy(p => p.Name).ToList())
            {
                var record = new GetSchoolView();
                record.Name = school.Name;
                record.Id = school.Id;
                foreach (var Teacher in school.Teachers.OrderBy(p => p.Name).ToList())
                {
                    record.Teachers.Add(new GetSchoolTeachers()
                    {
                        Name = Teacher.Name,
                        Id = Teacher.Id,
                        RoleId = Teacher.RoleId
                    });
                }
                result.Add(record);
            }
            return result;
        }
        public async Task<List<IdNumberNameView>> GetAllTeacherTitles()
        {
            var Titles = await unitOfWork.Repository<TeacherTitle, long>().GetAllAsync(new TeacherTitleSpecification());
            return Titles.Select(p => new IdNumberNameView()
            {
                Id = p.Id,
                Name = p.Title
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetTeachers(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId
            }));
            teachers = teachers.Where(p => p.RoleId == "Teacher" || p.RoleId == "TeacherSuper");
            if (teachers is null) throw new NotImplementedException("لا يوجد معلمين في هذه المدرسة");
            return teachers.OrderBy(p => p.Role.Index).ThenBy(p => p.Name).Select(p => new IdNumberNameView()
            {
                Name = p.Name,
                Id = p.Id
            }).ToList();
        }
        public async Task<List<IdNumberNameView>> GetAllTeachers(long SchoolId)
        {
            var teachers = await unitOfWork.Repository<Teacher, long>().GetAllAsync(new TeacherSpecification(new TeacherParams()
            {
                SchoolId = SchoolId
            }));
            teachers = teachers.Where(p => p.RoleId != "Manager");
            if (teachers is null) throw new NotImplementedException("لا يوجد معلمين في هذه المدرسة");
            return teachers.OrderBy(p => p.Role.Index).ThenBy(p => p.Name).Select(p => new IdNumberNameView()
            {
                Name = p.Name,
                Id = p.Id
            }).ToList();
        }
    }
}