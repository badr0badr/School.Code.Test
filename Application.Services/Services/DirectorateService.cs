﻿using Application.Core.Entities;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Core.Params;
using Application.Core.Specifications;
using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using Application.Core.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class DirectorateService : IDirectorateService
    {
        private readonly IUnitOfWork unitOfWork;
        public DirectorateService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<ErrorResponce> AddAgency(AddAgencyView view)
        {
            if (view is null) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            await unitOfWork.Repository<School, long>().AddAsync(new School()
            {
                Name = view.AgencyName,
                AdministrationId = 1, // to be changed later
            });
            var result = await unitOfWork.SaveChangesAsync();
            if (result == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var existingSchool = await unitOfWork.Repository<School, long>().GetByIdAsync(new SchoolSpecification(new SchoolParams()
            {
                Name = view.AgencyName
            }));
            await unitOfWork.Repository<Teacher, long>().AddAsync(new Teacher()
            {
                Name = view.AgencyMangerName,
                Password = "000000",
                RoleId = "Manager",
                SchoolId = existingSchool.Id
            });
            result = await unitOfWork.SaveChangesAsync();
            if (result == 0) return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
            var existingTeacher = await unitOfWork.Repository<Teacher, long>().GetByIdAsync(new TeacherSpecification(new TeacherParams()
            {
                Search = view.AgencyMangerName,
                SchoolId = existingSchool.Id
            }));
            existingSchool.SchoolMangerId = existingTeacher.Id;
            unitOfWork.Repository<School, long>().Update(existingSchool);
            result = await unitOfWork.SaveChangesAsync();
            if (result > 0) return new ErrorResponce(200, "تم اضافه الجهة");
            return new ErrorResponce(400, "تاكد من ادخال جميع البيانات");
        }
        public async Task<ErrorResponce> AddDirectorateUser(AddAgencyUserView view)
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

    }
}