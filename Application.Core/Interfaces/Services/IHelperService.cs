﻿using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IHelperService
    {
        Task<List<IdNumberNameView>> GetStudent(long ClassId);
        Task<List<IdNumberNameView>> GetSubjects(long? TeacherId);
        Task<List<IdNumberNameView>> GetSubjectsInSchool(long SchoolId);
        Task<List<IdNumberNameView>> GetSubjectsInSchoolHasApplied(long SchoolId);
        Task<List<IdNumberNameView>> GetSubjectsInSchoolWithType(IdNumberNameView view);
        Task<List<IdNumberNameView>> GetSubjectsSupervisior(long TeacherId);
        Task<List<IdNumberNameView>> GetClasses(long SchoolId);
        Task<List<string>> GetClassesNames(long SchoolId);
        Task<List<GetTeacherClassesView>> GetTeacherClasses(long TeacherId);
        Task<List<IdNumberNameView>> GetWeeks(int? Month);
        Task<List<IdStringNameView>> GetRolesForSchool();
        Task<List<IdStringNameView>> GetRolesForDirectorate();
        Task<List<IdStringNameView>> GetRolesForAdmin();
        Task<List<GetSchoolView>> GetSchools();
        Task<List<IdNumberNameView>> GetAllTeacherTitles();
        Task<List<IdNumberNameView>> GetTeachers(long SchoolId);
    }
}