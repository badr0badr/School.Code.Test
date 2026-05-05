﻿using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.Teacher;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Services
{
    public interface ITeacherService
    {
        Task<ErrorResponce> AddTeacher(AddTeacherView AddTeacher);
        Task<List<string>> GetTeacherSubjects(long TeacherId);
        Task<List<StudentForClassView>> GetStudentForClass(long ClassId);
        Task<ErrorResponce> DeleteTeacher(long TeacherId);
        Task<ErrorResponce> EditTeacher(EditTeacherView teacher);
        Task<ErrorResponce> AssignTeacherToClass(TeacherToClassView View);
        Task<List<GetTeachersView>> GetTeachers(long TeacherId);
        Task<ErrorResponce> AddClass(AddClassView view);
        Task<ErrorResponce> AddSubject(string SubjectName);
        Task<List<GetAssignTeacherView>> GetAssignTeacher(AssignTeacherView view);
        Task<ErrorResponce> DeleteAssign(long AssignId);
        Task<ErrorResponce> AddPA(AddPAView view);
        Task<List<IdNumberNameView>> GetPA(long SchoolId);
        Task<ErrorResponce> DeletePA(long PAId);
        Task<ErrorResponce> CheckManagement(CheckManagementView view);
        Task<ErrorResponce> ResetPassword(long TeacherId);
        Task<List<GetTeachersView>> GetAllWorkers(long SchoolId);
        Task<List<IdNumberNameView>> GetSuperSubjects(long TeacherId);
        Task<ErrorResponce> AddSuperSubjects(AddSuperSubjectsView view);
        Task<ErrorResponce> DeleteSuperSubjects(long SuperSubjectId);
        Task<List<IdNumberNameView>> GetSupervisors(long SchoolId);
        Task<List<IdNumberNameView>> SpearSubjects(long SchoolId);
        Task<List<IdNumberNameView>> GetNonSupervisors(long TeacherId);
        Task<ErrorResponce> ChangeSupervisor(ChangeSupervisorView view);
        Task<List<IdNumberNameView>> GetUnAssignClasses(AssignTeacherView view);
        Task<ErrorResponce> ChangeQualityControl(ChangeQuiltyControlView view);
        Task<IdStringNameView> GetQualityControlTeacherName(long SchoolId);
    }
}