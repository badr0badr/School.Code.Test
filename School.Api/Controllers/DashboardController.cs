﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace School.Api.Controllers
{
    public class DashboardController : BaseApiController
    {
        private readonly IDashboardService DashboardService;

        public DashboardController(IDashboardService _Dashboard)
        {
            DashboardService = _Dashboard;
        }
        [ProducesResponseType(typeof(FinishClassDashDataView), 200)]
        [HttpPost("FinishClassDashData")]
        public async Task<IActionResult> FinishClassDashData(RequestView<FinishClassDashView> view)
        {
            try
            {
                return Ok(await DashboardService.FinishClassDashData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(FinishClassDashDataView), 200)]
        [HttpPost("FinishTeacherClassDashData")]
        public async Task<IActionResult> FinishTeacherClassDashData(RequestView<FinishTeacherClassDashView> view)
        {
            try
            {
                return Ok(await DashboardService.FinishTeacherClassDashData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(WeeklyReportDataView), 200)]
        [HttpPost("GetWeeklyReport")]
        public async Task<IActionResult> GetWeeklyReport(RequestView<GetWeeklyReportView> view)
        {
            try
            {
                return Ok(await DashboardService.GetWeeklyReport(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MonthlyReportDataView), 200)]
        [HttpPost("GetMonthlyReport")]
        public async Task<IActionResult> GetMonthlyReport(RequestView<GetWeeklyReportView> view)
        {
            try
            {
                return Ok(await DashboardService.GetMonthlyReport(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(GetAverageReportDataView), 200)]
        [HttpPost("GetAverageReport")]
        public async Task<IActionResult> GetAverageReport(RequestView<GetWeeklyReportView> view)
        {
            try
            {
                return Ok(await DashboardService.GetAverageReport(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(StudentMonthlyReportDataView), 200)]
        [HttpPost("GetStudentMonthlyReport")]
        public async Task<IActionResult> GetStudentMonthlyReport(RequestView<GetStudentMonthlyReportView> view)
        {
            try
            {
                return Ok(await DashboardService.GetStudentMonthlyReport(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetVacationView>), 200)]
        [HttpPost("GetAdminVacation")]
        public async Task<IActionResult> GetAdminVacation()
        {
            try
            {
                return Ok(await DashboardService.GetAdminVacation());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetVacationView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetSchoolVacation")]
        public async Task<IActionResult> GetSchoolVacation(RequestView<long> view)
        {
            try
            {
                return Ok(await DashboardService.GetSchoolVacation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("VacationId")]
        [HttpPost("DeleteAdminVacation")]
        public async Task<IActionResult> DeleteAdminVacation(RequestView<long> view)
        {
            try
            {
                return Ok(await DashboardService.DeleteAdminVacation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("VacationId")]
        [HttpPost("DeleteSchoolVacation")]
        public async Task<IActionResult> DeleteSchoolVacation(RequestView<long> view)
        {
            try
            {
                return Ok(await DashboardService.DeleteSchoolVacation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(GetAllAverageMonthReport), 200)]
        [HttpPost("GetAllAverageMonthReportData")]
        public async Task<IActionResult> GetAllAverageMonthReportData(RequestView<GetAllAverageMonthReportView> view)
        {
            try
            {
                return Ok(await DashboardService.GetAllAverageMonthReportData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddAdminVacation")]
        public async Task<IActionResult> AddAdminVacation(RequestView<AddVacationView> view)
        {
            try
            {
                return Ok(await DashboardService.AddAdminVacation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddSchoolVacation")]
        public async Task<IActionResult> AddSchoolVacation(RequestView<AddVacationView> view)
        {
            try
            {
                return Ok(await DashboardService.AddSchoolVacation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(FinishClassDashExamDataView), 200)]
        [HttpPost("FinishClassDashExamData")]
        public async Task<IActionResult> FinishClassDashExamData(RequestView<FinishClassDashExamView> view)
        {
            try
            {
                return Ok(await DashboardService.FinishClassDashExamData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }

















        /////////DownLoad////////
        [HttpGet("Download/{FileName}")]
        public IActionResult DownloadFile(string FileName)
        {
            try
            {
                var FolderName = Path.Combine("wwwroot", "Resources", "PDFs");
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
                if (!System.IO.File.Exists(FilePath))
                {
                    return NotFound(new ApiError(StatusCodes.Status404NotFound, "بالرجاء اعادة فتح التقرير مره اخري"));
                }
                string MimeType = GetContentType(FilePath);
                var FileBytes = System.IO.File.ReadAllBytes(FilePath);
                var file = File(FileBytes, MimeType, FileName);
                return file;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        private string GetContentType(string Path)
        {
            var Provider = new FileExtensionContentTypeProvider();
            if (!Provider.TryGetContentType(Path, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}