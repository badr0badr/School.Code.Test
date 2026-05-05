﻿using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface ISupervisorService
    {
        Task<List<TeacherTitleView>> GetAllTeacherTitles();
        Task<List<ClassSubjectShareView>> GetAllClassSubjectShare();
        Task<ClassSubjectShareView> GetClassesShareForSubject(long SubjectId);
        Task<SubjectClassShareView> GetSubjectShareForClass(string ClassName);
        Task<ErrorResponce> AddOrUpdateClassSubjectShare(AddOrUpdateClassSubjectShareView view);
        Task<SchoolSharePowerView> SchoolSharePower(long SchoolId);
        Task<ClassSharePowerView> ClassSharePower(long SchoolId);
        Task<SummaryShareView> SummaryShare(long SchoolId);
        Task<List<SummaryShareDetailsView>> SummaryShareDetails(long SchoolId);
        Task<SubjectSummaryShareDetails> SubjectSummaryShareDetails(SubjectSummaryShareDetailsView view);
        Task<List<IdNumberNameView>> GetTeachers(GetTeachersVisionView view);
        Task<List<DailySupervisionView>> DailySupervision(long SchoolId);
        Task<List<IdStringNameView>> GeneralSupervision(long SchoolId);
        Task<ErrorResponce> ModifySupervision(ModifySupervisionView view);
        Task<SubjectMapDetails> SubjectMap(SubjectMapView view);
        Task<TitelsShareViewData> TitlesShare(SubjectMapView view);
        Task<ClassSharePowerView> ClassShareForSubject(SubjectMapView view);
    }
}