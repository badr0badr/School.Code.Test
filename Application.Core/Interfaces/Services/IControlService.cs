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
    public interface IControlService
    {
        Task<List<IdStringNameView>> GetControlTypes();
        Task<List<ControlTeachersData>> GetControlTeachers(long SchoolId);
        Task<ErrorResponce> AddToControl(AddToControlView view);
        Task<ErrorResponce> DeleteFromControl(long TeacherId);
        Task<ErrorResponce> AddPlaceNumbers(long SchoolId);
        Task<ErrorResponce> DeletePlaceNumbers(long SchoolId);
        Task<ErrorResponce> GetStudentCountInExamHall(StudentExamHallView view);
        Task<ErrorResponce> AddStudentInExamHall(StudentExamHallView view);
        Task<ErrorResponce> DeleteStudentFromExamHall(DeleteStudentExamView view);
        Task<ErrorResponce> AddSecriteCodes(long SchoolId);
        Task<ErrorResponce> DeleteSecriteCodes(long SchoolId);
        Task<StudentsPlaceNumbers> GetPlaceNumbers(GetPlaceNumbersView view);
        Task<ErrorResponce> PreparingExam(PreparingFinalExamView view);
        Task<ErrorResponce> DeletePreparingExam(long SchoolId);
        Task<List<MerrorPdfData>> MerrorData(MerrorDataView view);
        Task<List<ExamTempltesModelData>> GetExamTempltesModel(long schoolId);
        Task<FinalAvargeData> GetSheetPaper(FinalAvargeView view);
    }
}