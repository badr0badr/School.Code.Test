﻿using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<FinishClassDashDataView> FinishClassDashData(FinishClassDashView view);
        Task<FinishClassDashDataView> FinishTeacherClassDashData(FinishTeacherClassDashView view);
        Task<WeeklyReportDataView> GetWeeklyReport(GetWeeklyReportView view);
        Task<MonthlyReportDataView> GetMonthlyReport(GetWeeklyReportView view);
        Task<GetAverageReportDataView> GetAverageReport(GetWeeklyReportView view);
        Task<StudentMonthlyReportDataView> GetStudentMonthlyReport(GetStudentMonthlyReportView View);
        Task<List<GetVacationView>> GetAdminVacation();
        Task<List<GetVacationView>> GetSchoolVacation(long SchoolId);
        Task<ErrorResponce> DeleteAdminVacation(long VacationId = 0);
        Task<ErrorResponce> DeleteSchoolVacation(long VacationId = 0);
        Task<ErrorResponce> AddAdminVacation(AddVacationView view);
        Task<ErrorResponce> AddSchoolVacation(AddVacationView view);
        Task<GetAllAverageMonthReport> GetAllAverageMonthReportData(GetAllAverageMonthReportView view);
        Task<FinishClassDashExamDataView> FinishClassDashExamData(FinishClassDashExamView view);
    }
}