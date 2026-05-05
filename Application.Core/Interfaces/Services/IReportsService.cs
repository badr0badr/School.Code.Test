﻿using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IReportsService
    {
        Task<AvargeForClassData> GetAverageForClass(StudentForClassScoreView view);
        Task<ErrorResponce> GetStudentsCode(long SchoolId);
        Task<TotalAvrageViewClassData> GetAvrageView(AvrageView view);
        Task<TotalClassAvrage> GetClassAvrageView(ClassAvrageView view);
        Task<PreparingExamModel> PreparingExam(PreparingExamView view);
        Task<StudentCertificateData> StudentCertificate(StudentCertificateView view);
        Task<ErrorResponce> AddCertificateSetting(CertificateSettingView view);
        Task<ErrorResponce> DeleteCertificateSetting(long CertificateSettingId);
        Task<List<StudentMonthlyCertificateSettingData>> GetCertificateSettings(long SchoolId);
        Task<AdvErrorResponce<List<string>>> StartExamSave(long CertificateSettingId);
        Task<AdvErrorResponce<List<string>>> CreateCertificate(long CertificateSettingId);
        Task<CertificateReportData> CertificateReport(CertificateReportView view);
        Task<ClassDigramData> ClassDigram(ClassDigramView view);
    }
}