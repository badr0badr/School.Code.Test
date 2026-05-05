﻿using Application.Core.Entities;
using Application.Core.Helper;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Auth;
using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using Application.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;
        private readonly AppDbContext context;
        private readonly IParentService parentService;

        public AuthService(IUnitOfWork _unitOfWork, ITokenService _tokenService, AppDbContext _context, IParentService _parentService)
        {
            unitOfWork = _unitOfWork;
            tokenService = _tokenService;
            context = _context;
            parentService = _parentService;
        }
        public async Task<TeacherData> TeacherLogin(LoginView login)
        {
            if (login.Password == null) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            if (login.Id == null || login.Id == 0) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            var user = await unitOfWork.Repository<Teacher, long>().GetByIdNoTrackingAsync(new TeacherSpecification(login.Id.Value));
            if (user == null) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            if (user.RoleId == "guest") return await GetUserData(user);
            if (user.Password != login.Password) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            return await GetUserData(user);
        }
        public async Task<TeacherData> ControlTeacherLogin(LoginView login)
        {
            if (login.Password == null) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            if (login.Id == null || login.Id == 0) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            var user = await unitOfWork.Repository<Teacher, long>().GetByIdNoTrackingAsync(new TeacherSpecification(login.Id.Value));
            if (user == null) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            if (user.Password != login.Password) throw new Exception("تأكد من اختيار اسم المستخدم و كتابة الرقم السري");
            if (user.CanAccesControl) return await GetUserData(user);
            throw new Exception("لا يمكن الدخول إلا للعاملين بالكنترول");
        }
        private async Task<TeacherData> GetUserData(Teacher user)
        {
            var IsSuper = false;
            var NowDate = DateOnly.FromDateTime(DateTime.Now);
            var DayName = DateHelper.GetDayName(DateTime.Now);
            var General = await unitOfWork.Repository<GeneralSupervision, long>().GetAllAsync(new GeneralSupervisionSpecification());
            IsSuper = General.Any(p => p.TeacherId == user.Id && p.SchoolId == user.SchoolId && p.Day == DateHelper.DayNameAr(DayName));
            if (IsSuper == false)
            {
                var dayly = await unitOfWork.Repository<DailySupervision, long>().GetAllAsync(new DailySupervisionSpecification());
                if (DayName == "Saturday")
                {
                    IsSuper = dayly.Any(p => p.Saturday == user.Id && p.SchoolId == user.SchoolId);
                }
                else if (DayName == "Sunday")
                {
                    IsSuper = dayly.Any(p => p.Sunday == user.Id && p.SchoolId == user.SchoolId);
                }
                else if (DayName == "Monday")
                {
                    IsSuper = dayly.Any(p => p.Monday == user.Id && p.SchoolId == user.SchoolId);
                }
                else if (DayName == "Tuesday")
                {
                    IsSuper = dayly.Any(p => p.Tuseday == user.Id && p.SchoolId == user.SchoolId);
                }
                else if (DayName == "Wednesday")
                {
                    IsSuper = dayly.Any(p => p.Wednesday == user.Id && p.SchoolId == user.SchoolId);
                }
                else if (DayName == "Thursday")
                {
                    IsSuper = dayly.Any(p => p.Thursday == user.Id && p.SchoolId == user.SchoolId);
                }
            }
            return new TeacherData
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.RoleId,
                RoleName = user.Role.RoleArabic,
                SchoolId = user.SchoolId,
                Role2 = user.RoleId2,
                HasQuiltyControl = user.HasQuiltyControl,
                Role2Name = user.Role2 == null ? null : user.Role2.RoleArabic,
                MainSubject = user.MainSubjectId ?? 0,
                HasSupervision = IsSuper,
                HasControl = user.ControlType != null,
                ControlType = user.ControlType ?? "null",
                Token = tokenService.CreateToken(user)
            };
        }
        public async Task<TeacherData> TeacherDataByToken(string Id)
        {
            var data = tokenService.GetTeacherFromToken(Id);
            if (data == null) throw new NotImplementedException("مستخدم غير معروف");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(data.Id));
            if (teacher == null) throw new NotImplementedException("مستخدم غير معروف");
            //if (string.IsNullOrEmpty(teacher.Color)) throw new NotImplementedException("Upadte Info");
            return await GetUserData(teacher);
        }
        public async Task<TeacherData> ControlTeacherDataByToken(string Id)
        {
            var data = tokenService.GetTeacherFromToken(Id);
            if (data == null) throw new NotImplementedException("مستخدم غير معروف");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(data.Id));
            if (teacher == null) throw new NotImplementedException("مستخدم غير معروف");
            //if (string.IsNullOrEmpty(teacher.Color)) throw new NotImplementedException("Upadte Info");
            if (teacher.CanAccesControl) return await GetUserData(teacher);
            throw new Exception("لا يمكن الدخول إلا للعاملين بالكنترول");
        }
        public async Task<ErrorResponce> Signin(SigninView view)
        {
            if (view == null) return new ErrorResponce(400, "ادخل جميع البيانات");
            if (view.Password != view.RePassword) return new ErrorResponce(400, "كلمة المرور غير متطابقة");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.Id));
            if (teacher is null) return new ErrorResponce(600, "خطأ");
            teacher.Password = view.Password;
            teacher.Color = view.Color;
            unitOfWork.Repository<Teacher, long>().Update(teacher);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<PerantData> PerantLogin(long Code)
        {
            var user = await unitOfWork.Repository<Student, long>().GetByIdNoTrackingAsync(new StudentSpecification(new StudentParams()
            {
                Code = Code
            }, false));
            if (user == null) throw new Exception("الكود الذي ادخلتة خطأ");
            return new PerantData()
            {
                Code = user.Code,
                Id = user.Id,
                Name = user.Name,
                Token = tokenService.CreateToken(user.Id)
            };
        }
        public async Task<PerantData> PerantDataByToken(string Id)
        {
            var data = tokenService.GetPerantFromToken(Id);
            if (data == null) throw new Exception("مستخدم غير معروف");
            var user = await unitOfWork.Repository<Student, long>().GetByIdNoTrackingAsync(new StudentSpecification(data.Value, false));
            if (user == null) throw new Exception("مستخدم غير معروف");
            var Abscount = await parentService.GetStudentProfile(user.Id);
            return new PerantData()
            {
                Code = user.Code,
                Id = user.Id,
                Name = user.Name,
                StudentAbsent = Abscount.WeekAbsent,
                Token = tokenService.CreateToken(user.Id)
            };
        }
        public async Task<GetProfileView> GetProfile(long Id)
        {
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(Id));
            var subjects = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                TeacherId = Id
            }));
            var Class = await unitOfWork.Repository<Classes, long>().GetAllAsync(new ClassesSpecification());
            var result = new GetProfileView();
            result.Id = teacher.Id;
            result.Name = teacher.Name;
            result.RoleName = teacher.Role.RoleArabic;
            result.SchoolName = teacher.School.Name;
            result.SchoolId = teacher.School.Id;
            foreach (var item in teacher.Classes.Select(p => p.ClassId))
            {
                result.Classes.Add(Class.FirstOrDefault(p => p.Id == item).FullId);
            }
            result.Classes = teacher.Classes.Select(p => p.Class.FullId).ToList();
            result.Subjects = subjects.DistinctBy(p => p.SubjectId).Select(p => p.Subject.Name).ToList();
            /*if (teacher.MangerId.HasValue)
            {
                result.MangerId = teacher.MangerId.Value == Id ? 0 : teacher.MangerId.Value;
                result.MangerName = teacher.MangerId == Id ? "" : teacher.Manager.Name;
            }*/
            return result;
        }
    }
}