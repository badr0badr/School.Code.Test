﻿using Application.Core.Views.Other;
using Application.Core.Views.Student;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<ErrorResponce> AddStudent(AddStudentView Addstudent);
        Task<ErrorResponce> EditStudentRange(List<EditStudentView> view);
        Task<ErrorResponce> EditStudentClass(EditStudentView student);
        Task<List<ClassStudentsView>> GetClassStudents(long ClassId);
        Task<ErrorResponce> AddStudentRange(List<AddStudentView> Addstudent);
        Task<ErrorResponce> EditStudentAbsentDays(EditStudentAbsentDaysView view);
        Task<ErrorResponce> DeleteStudent(long StudentId);
        Task<ErrorResponce> EditStudent(EditStudentView student);
        Task<ErrorResponce> DeleteClass(long ClassId);
        Task<ErrorResponce> RestoreStudent(long StudentId);
        Task<ErrorResponce> SoftDeleteStudent(long StudentId);
        Task<List<IdNumberNameView>> GetDeletedStudents();
        Task<ErrorResponce> SendStudentToAnotherSchool(SendStudentToAnotherSchoolView view);
        Task<List<GetTransformationsView>> GetStudentTransformationsReceiver(long SchoolId);
        Task<List<GetTransformationsView>> GetStudentTransformationsSender(long SchoolId);
        Task<ErrorResponce> AcceptStudentTransformation(AcceptStudentTransformationView view);
        Task<GetAllStudentStaticsSchoolData> GetAllStudentStatics(long SchoolId);
        Task<List<UploadedStudentNames>> UploadStudentNames(IFormFile file);
        Task<ErrorResponce> UploadStudentCodes(IFormFile file);
    }
}