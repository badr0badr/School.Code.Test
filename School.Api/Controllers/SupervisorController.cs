﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class SupervisorController : BaseApiController
    {
        private readonly ISupervisorService supervisorService;

        public SupervisorController(ISupervisorService _supervisorService)
        {
            supervisorService = _supervisorService;
        }
        [ProducesResponseType(typeof(List<TeacherTitleView>), 200)]
        [HttpPost("GetAllTeacherTitles")]
        public async Task<IActionResult> GetAllTeacherTitles()
        {
            try
            {
                return Ok(await supervisorService.GetAllTeacherTitles());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<ClassSubjectShareView>), 200)]
        [HttpPost("GetAllClassSubjectShare")]
        public async Task<IActionResult> GetAllClassSubjectShare()
        {
            try
            {
                return Ok(await supervisorService.GetAllClassSubjectShare());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ClassSubjectShareView), 200)]
        [EndpointSummary("SubjectId")]
        [HttpPost("GetClassesShareForSubject")]
        public async Task<IActionResult> GetClassesShareForSubject(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.GetClassesShareForSubject(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(SubjectClassShareView), 200)]
        [EndpointSummary("ClassName")]
        [HttpPost("GetSubjectShareForClass")]
        public async Task<IActionResult> GetSubjectShareForClass(RequestView<string> view)
        {
            try
            {
                return Ok(await supervisorService.GetSubjectShareForClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(SchoolSharePowerView), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("SchoolSharePower")]
        public async Task<IActionResult> SchoolSharePower(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.SchoolSharePower(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ClassSharePowerView), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("ClassSharePower")]
        public async Task<IActionResult> ClassSharePower(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.ClassSharePower(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(SummaryShareView), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("SummaryShare")]
        public async Task<IActionResult> SummaryShare(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.SummaryShare(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<SummaryShareDetailsView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("SummaryShareDetails")]
        public async Task<IActionResult> SummaryShareDetails(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.SummaryShareDetails(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddOrUpdateClassSubjectShare")]
        public async Task<IActionResult> AddOrUpdateClassSubjectShare(RequestView<AddOrUpdateClassSubjectShareView> view)
        {
            try
            {
                return Ok(await supervisorService.AddOrUpdateClassSubjectShare(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(SubjectSummaryShareDetails), 200)]
        [HttpPost("SubjectSummaryShareDetails")]
        public async Task<IActionResult> SubjectSummaryShareDetails(RequestView<SubjectSummaryShareDetailsView> view)
        {
            try
            {
                return Ok(await supervisorService.SubjectSummaryShareDetails(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [HttpPost("GetTeachers")]
        public async Task<IActionResult> GetTeachers(RequestView<GetTeachersVisionView> view)
        {
            try
            {
                return Ok(await supervisorService.GetTeachers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(DailySupervisionView), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("DailySupervision")]
        public async Task<IActionResult> DailySupervision(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.DailySupervision(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdStringNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GeneralSupervision")]
        public async Task<IActionResult> GeneralSupervision(RequestView<long> view)
        {
            try
            {
                return Ok(await supervisorService.GeneralSupervision(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ModifySupervision")]
        public async Task<IActionResult> ModifySupervision(RequestView<ModifySupervisionView> view)
        {
            try
            {
                return Ok(await supervisorService.ModifySupervision(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(SubjectMapDetails), 200)]
        [HttpPost("SubjectMap")]
        public async Task<IActionResult> SubjectMap(RequestView<SubjectMapView> view)
        {
            try
            {
                return Ok(await supervisorService.SubjectMap(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(TitelsShareViewData), 200)]
        [HttpPost("TitlesShare")]
        public async Task<IActionResult> TitlesShare(RequestView<SubjectMapView> view)
        {
            try
            {
                return Ok(await supervisorService.TitlesShare(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ClassSharePowerView), 200)]
        [HttpPost("ClassShareForSubject")]
        public async Task<IActionResult> ClassShareForSubject(RequestView<SubjectMapView> view)
        {
            try
            {
                return Ok(await supervisorService.ClassShareForSubject(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}