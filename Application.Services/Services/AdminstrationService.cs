﻿using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Adminstration;
using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class AdminstrationService : IAdminstrationService
    {
        private readonly IUnitOfWork unitOfWork;
        public AdminstrationService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<ErrorResponce> AddAdminstrationUser(AddAgencyUserView view)
        {
            if (view is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var existingTeacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(new TeacherParams()
            {
                Search = view.Name,
                SchoolId = view.SchoolId
            }));
            if (existingTeacher is not null) return new ErrorResponce(400, "هذا الاسم موجود");
            await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
            {
                Name = view.Name,
                Password = "000000",
                RoleId = view.RoleId,
                SchoolId = view.SchoolId
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه مستخدم للجهة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> TransferTeacher(TransferTeacherView view)
        {
            if (view is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var teacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(view.TeacherId));
            if (teacher is null) return new ErrorResponce(400, "المعلم غير موجود");
            teacher.SchoolId = view.NewSchoolId;
            unitOfWork.Repository<Teacher, long>().Update(teacher);
            var TCs = await unitOfWork.Repository<TeacherClasses, long>().GetAllAsync(new TeacherClassesSpecification(new TeacherClassesParams()
            {
                TeacherId = view.TeacherId,
            }));
            unitOfWork.Repository<TeacherClasses, long>().DeleteRange(TCs.Where(p => p.Class.SchoolId == view.OldSchoolId).ToList());
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم نقل المعلم بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AssignmentTeacherToSchool(AssignTeacherToSchoolView view)
        {
            if (view is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            await unitOfWork.Repository<Assignment, long>().AddAsync(new Assignment()
            {
                TeacherId = view.TeacherId,
                SchoolId = view.SchoolId,
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم ندب المعلم بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> ConfirmStudentTransfer(long TransformationId)
        {
            if (TransformationId <= 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var transformation = await unitOfWork.Repository<SendStudentToSchool, long>().GetByIdAsync(new SendStudentToSchoolSpecification(TransformationId));
            if (transformation is null) return new ErrorResponce(400, "طلب النقل غير موجود");
            transformation.CanBeSend = true;
            unitOfWork.Repository<SendStudentToSchool, long>().Update(transformation);
            var result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم تاكيد طلب النقل بنجاح");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
    }
}