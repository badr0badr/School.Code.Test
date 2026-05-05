﻿using Application.Core.Entities;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.TekStatics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface ITekStaticsService
    {
        Task<ClassSheetPdf> GetClassSheets(ClassSheetPdfView view);
        Task<Get5BehaviorView> Get5Behavior(BehaviorView view);
        Task<MonitorGradesDetails> MonitorGrades(MonitorGradesView view);
        Task<MonitorGradesDataDetails> MonitorGradesData(StudentForClassScoreView view);
        Task<ErrorResponce> RestMonitorGrades(StudentForClassScoreView view);
        Task<ErrorResponce> ConfirmMonitorGrades(StudentForClassScoreView view);
        Task<MonitorGradesDetails> GetMonitorExamGrades(StudentForExamGradeView view);
        Task<MonitotExamGradesDataDetails> MonitorExamGradesData(StudentForExamScoreView view);
        Task<ErrorResponce> RestMonitorExamGrades(StudentForExamScoreView view);
        Task<ErrorResponce> ConfirmMonitorExamGrades(StudentForExamScoreView view);
        Task<List<StudentClassGPA>> TestConfirm(long subjectId);
    }
}