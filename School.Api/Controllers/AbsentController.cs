﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.Teacher;
using Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class AbsentController : BaseApiController
    {
        private readonly IAbsentService absentService;
        public AbsentController(IAbsentService _absentService)
        {
            absentService = _absentService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("IsAbsentClassExist")]
        public async Task<IActionResult> IsAbsentClassExist(RequestView<IsAbsentClassExistView> view)
        {
            try
            {
                return Ok(await absentService.IsAbsentClassExist(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SaveAbsentStudents")]
        public async Task<IActionResult> SaveAbsentStudents(RequestView<SaveAbsentStudentsView> view)
        {
            try
            {
                return Ok(await absentService.SaveAbsentStudents(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("DeleteAbsent")]
        public async Task<IActionResult> DeleteAbsent(RequestView<long> view)
        {
            try
            {
                return Ok(await absentService.DeleteAbsent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentAbsentDataExist>), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("GetStudentAbsent")]
        public async Task<IActionResult> GetStudentAbsent(RequestView<long> view)
        {
            try
            {
                return Ok(await absentService.GetStudentAbsent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MainAllAbsentView), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetAllAbsentView")]
        public async Task<IActionResult> GetAllAbsentView(RequestView<long> view)
        {
            try
            {
                return Ok(await absentService.GetAllAbsentView(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<AbsentStudentView>), 200)]
        [EndpointSummary("StudentAbsentId")]
        [HttpPost("GetAbsentViewById")]
        public async Task<IActionResult> GetAbsentViewById(RequestView<long> view)
        {
            try
            {
                return Ok(await absentService.GetAbsentViewById(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetAllAbsentDataView>), 200)]
        [HttpPost("GetAllAbsentViewData")]
        public async Task<IActionResult> GetAllAbsentViewData(RequestView<GetAllAbsentViewDataView> view)
        {
            try
            {
                return Ok(await absentService.GetAllAbsentViewData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetAllAbsentDataView>), 200)]
        [HttpPost("GetTeacherAbsentViewData")]
        public async Task<IActionResult> GetTeacherAbsentViewData(RequestView<GetAllAbsentViewDataView> view)
        {
            try
            {
                return Ok(await absentService.GetTeacherAbsentViewData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SaveResetAbsentData")]
        public async Task<IActionResult> SaveResetAbsentData(RequestView<List<AbsentStudentView>> view)
        {
            try
            {
                return Ok(await absentService.SaveResetAbsentData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(Get5BehaviorView), 200)]
        [HttpPost("Get5Behavior")]
        public async Task<IActionResult> Get5Behavior(RequestView<BehaviorView> view)
        {
            try
            {
                return Ok(await absentService.Get5Behavior(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentAbsentDataExist>), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("GetStudentExamAbsent")]
        public async Task<IActionResult> GetStudentExamAbsent(RequestView<long> view)
        {
            try
            {
                return Ok(await absentService.GetStudentExamAbsent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SaveAbsentStudentsExam")]
        public async Task<IActionResult> SaveAbsentStudentsExam(RequestView<SaveAbsentStudentsExamView> view)
        {
            try
            {
                return Ok(await absentService.SaveAbsentStudentsExam(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ExamAbsentData), 200)]
        [HttpPost("GetExamAbsentData")]
        public async Task<IActionResult> GetExamAbsentData(RequestView<ExamAbsentDataView> view)
        {
            try
            {
                return Ok(await absentService.GetExamAbsentData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}