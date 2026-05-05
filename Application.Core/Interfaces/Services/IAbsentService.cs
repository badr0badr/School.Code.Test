﻿using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IAbsentService
    {
        Task<ErrorResponce> IsAbsentClassExist(IsAbsentClassExistView View);
        Task<List<StudentAbsentDataExist>> GetStudentAbsent(long ClassId);
        Task<ErrorResponce> DeleteAbsent(long ClassId);
        Task<ErrorResponce> SaveAbsentStudents(SaveAbsentStudentsView View);
        Task<MainAllAbsentView> GetAllAbsentView(long SchoolId);
        Task<List<AbsentStudentView>> GetAbsentViewById(long Id);
        Task<List<GetAllAbsentDataView>> GetAllAbsentViewData(GetAllAbsentViewDataView view);
        Task<List<GetAllAbsentDataView>> GetTeacherAbsentViewData(GetAllAbsentViewDataView view);
        Task<ErrorResponce> SaveResetAbsentData(List<AbsentStudentView> view);
        Task<Get5BehaviorView> Get5Behavior(BehaviorView view);
        Task<List<StudentAbsentDataExist>> GetStudentExamAbsent(long ClassId);
        Task<ErrorResponce> SaveAbsentStudentsExam(SaveAbsentStudentsExamView View);
        Task<ExamAbsentData> GetExamAbsentData(ExamAbsentDataView view);
    }
}