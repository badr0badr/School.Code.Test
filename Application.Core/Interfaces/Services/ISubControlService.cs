﻿using Application.Core.Views.Control;
using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface ISubControlService
    {
        Task<List<StudentsForAppliedExamData>> GetStudentsForAppliedExam(StudentsForAppliedExamView view);
        Task<ErrorResponce> SaveStudentsForAppliedExam(SaveStudentsForAppliedExamView view);
        Task<ErrorResponce> CheakCode(string Token, int Code);
        Task<ErrorResponce> AddExamResult(string Token, AddExamResultView view);
        Task<ErrorResponce> ViewStudentData(string Token, int Code);
        Task<List<string>> GetAllFileNamesInFolder();
    }
}