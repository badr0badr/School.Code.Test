﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class HelperController : BaseApiController
    {
        private readonly IHelperService helperService;
        public HelperController(IHelperService _helperService)
        {
            helperService = _helperService;
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("GetStudent")]
        public async Task<IActionResult> GetStudent(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("TeacherId? :All")]
        [HttpPost("GetSubjects")]
        public async Task<IActionResult> GetSubjects(RequestView<long?> view)
        {
            try
            {
                return Ok(await helperService.GetSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetSubjectsSupervisior")]
        public async Task<IActionResult> GetSubjectsSupervisior(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetSubjectsSupervisior(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetClasses")]
        public async Task<IActionResult> GetClasses(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetClasses(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<string>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetClassesNames")]
        public async Task<IActionResult> GetClassesNames(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetClassesNames(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetTeacherClassesView>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetTeacherClasses")]
        public async Task<IActionResult> GetTeacherClasses(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetTeacherClasses(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("Month   if=13 Get All")]
        [HttpPost("GetWeeks")]
        public async Task<IActionResult> GetWeeks(RequestView<int?> view)
        {
            try
            {
                return Ok(await helperService.GetWeeks(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetTeachers")]
        public async Task<IActionResult> GetTeachers(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetTeachers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetSubjectsInSchool")]
        public async Task<IActionResult> GetSubjectsInSchool(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetSubjectsInSchool(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetSubjectsInSchoolHasApplied")]
        public async Task<IActionResult> GetSubjectsInSchoolHasApplied(RequestView<long> view)
        {
            try
            {
                return Ok(await helperService.GetSubjectsInSchoolHasApplied(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetSubjectsInSchoolWithType")]
        public async Task<IActionResult> GetSubjectsInSchoolWithType(RequestView<IdNumberNameView> view)
        {
            try
            {
                return Ok(await helperService.GetSubjectsInSchoolWithType(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdStringNameView>), 200)]
        [HttpPost("GetRolesForSchool")]
        public async Task<IActionResult> GetRolesForSchool()
        {
            try
            {
                return Ok(await helperService.GetRolesForSchool());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdStringNameView>), 200)]
        [HttpPost("GetRolesForAdmin")]
        public async Task<IActionResult> GetRolesForAdmin()
        {
            try
            {
                return Ok(await helperService.GetRolesForAdmin());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdStringNameView>), 200)]
        [HttpPost("GetRolesForDirectorate")]
        public async Task<IActionResult> GetRolesForDirectorate()
        {
            try
            {
                return Ok(await helperService.GetRolesForDirectorate());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetSchoolView>), 200)]
        [HttpPost("GetSchools")]
        public async Task<IActionResult> GetSchools()
        {
            try
            {
                return Ok(await helperService.GetSchools());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [HttpPost("GetAllTeacherTitles")]
        public async Task<IActionResult> GetAllTeacherTitles()
        {
            try
            {
                return Ok(await helperService.GetAllTeacherTitles());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}