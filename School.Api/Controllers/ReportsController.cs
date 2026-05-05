﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class ReportsController : BaseApiController
    {
        private readonly IReportsService reportsService;
        public ReportsController(IReportsService _reportsService)
        {
            reportsService = _reportsService;
        }
        [ProducesResponseType(typeof(AvargeForClassData), 200)]
        [HttpPost("GetAverageForClass")]
        public async Task<IActionResult> GetStudent(RequestView<StudentForClassScoreView> view)
        {
            try
            {
                return Ok(await reportsService.GetAverageForClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetStudentsCode")]
        public async Task<IActionResult> GetStudentsCode(RequestView<long> view)
        {
            try
            {
                return Ok(await reportsService.GetStudentsCode(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(TotalAvrageViewClassData), 200)]
        [HttpPost("GetAvrageView")]
        public async Task<IActionResult> GetAvrageView(RequestView<AvrageView> view)
        {
            try
            {
                return Ok(await reportsService.GetAvrageView(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(TotalClassAvrageDetails), 200)]
        [HttpPost("GetClassAvrageView")]
        public async Task<IActionResult> GetClassAvrageView(RequestView<ClassAvrageView> view)
        {
            try
            {
                return Ok(await reportsService.GetClassAvrageView(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(PreparingExamModel), 200)]
        [HttpPost("PreparingExam")]
        public async Task<IActionResult> PreparingExam(RequestView<PreparingExamView> view)
        {
            try
            {
                return Ok(await reportsService.PreparingExam(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(StudentCertificateData), 200)]
        [HttpPost("StudentCertificate")]
        public async Task<IActionResult> StudentCertificate(RequestView<StudentCertificateView> view)
        {
            try
            {
                return Ok(await reportsService.StudentCertificate(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddCertificateSetting")]
        public async Task<IActionResult> AddCertificateSetting(RequestView<CertificateSettingView> view)
        {
            try
            {
                return Ok(await reportsService.AddCertificateSetting(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("CertificateSettingId")]
        [HttpPost("DeleteCertificateSetting")]
        public async Task<IActionResult> DeleteCertificateSetting(RequestView<long> view)
        {
            try
            {
                return Ok(await reportsService.DeleteCertificateSetting(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(AdvErrorResponce<List<string>>), 200)]
        [EndpointSummary("CertificateSettingId")]
        [HttpPost("StartExamSave")]
        public async Task<IActionResult> StartExamSave(RequestView<long> view)
        {
            try
            {
                return Ok(await reportsService.StartExamSave(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(AdvErrorResponce<List<string>>), 200)]
        [EndpointSummary("CertificateSettingId")]
        [HttpPost("CreateCertificate")]
        public async Task<IActionResult> CreateCertificate(RequestView<long> view)
        {
            try
            {
                return Ok(await reportsService.CreateCertificate(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(StudentMonthlyCertificateSettingData), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetCertificateSettings")]
        public async Task<IActionResult> GetCertificateSettings(RequestView<long> view)
        {
            try
            {
                return Ok(await reportsService.GetCertificateSettings(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(CertificateReportData), 200)]
        [HttpPost("CertificateReport")]
        public async Task<IActionResult> CertificateReport(RequestView<CertificateReportView> view)
        {
            try
            {
                return Ok(await reportsService.CertificateReport(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ClassDigramData), 200)]
        [HttpPost("ClassDigram")]
        public async Task<IActionResult> ClassDigram(RequestView<ClassDigramView> view)
        {
            try
            {
                return Ok(await reportsService.ClassDigram(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}