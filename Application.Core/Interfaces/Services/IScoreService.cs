﻿using Application.Core.Views.Other;
using Application.Core.Views.Score;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Services
{
    public interface IScoreService
    {
        Task<ErrorResponce> AddScores(AddScoresView View);
        Task<ErrorResponce> AddExamScores(AddScoresView View);
        Task<ErrorResponce> IsClassExist(AddScoresView View);
        Task<ClassExamData> IsClassExamExist(long TeacherClassesId);
        Task<List<GetStudentForClassScore>> GetStudentForClassScore(StudentForClassScoreView view);
        Task<List<ScoreForPreAbsentData>> ScoreForPreAbsent(ScoreForPreAbsentView view);
        Task<ErrorResponce> EditStudentScore(EditStudentScoreView view);
        Task<ErrorResponce> AddEditScores(AddEditScoresView View);
    }
}