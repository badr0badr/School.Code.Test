﻿using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface ISubControlService
    {
        Task<List<StudentsForAppliedExamData>> GetStudentsForAppliedExam(string Token, StudentsForAppliedExamView view);
        Task<ErrorResponce> SaveStudentsForAppliedExam(string Token, SaveStudentsForAppliedExamView view);
        Task<ErrorResponce> CheakCode(string Token, long Code);
        Task<ErrorResponce> AddExamResult(string Token, AddExamResultView view);
        Task<ErrorResponce> ViewStudentData(string Token, long Code);
        Task<List<string>> GetAllFileNamesInFolder(string Token);
        Task<List<HallSammryData>> HallSummryDatas(string Token, long SchoolId);
        Task<List<StudentTackit>> GetStudentTackits(string Token, long SchoolId);
        Task<List<StudentExamAbsentData>> GetExamAbsent(string Token, GetExamAbsentView view);
        Task<ErrorResponce> SaveExamAbsent(string Token, SaveExamAbsentView view);
        Task<HandlingExamPapersToRateData> HandlingExamPapersToRate(string Token, HandlingExamPapersToRateView view);
    }
}