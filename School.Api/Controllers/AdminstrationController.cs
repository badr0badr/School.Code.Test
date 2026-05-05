﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Adminstration;
using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class AdminstrationController : BaseApiController
    {
        private readonly IAdminstrationService adminstrationService;
        public AdminstrationController(IAdminstrationService _adminstrationService)
        {
            adminstrationService = _adminstrationService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddAdminstrationUser")]
        public async Task<IActionResult> AddAdminstrationUser(RequestView<AddAgencyUserView> view)
        {
            try
            {
                return Ok(await adminstrationService.AddAdminstrationUser(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("TransferTeacher")]
        public async Task<IActionResult> TransferTeacher(RequestView<TransferTeacherView> view)
        {
            try
            {
                return Ok(await adminstrationService.TransferTeacher(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AssignmentTeacherToSchool")]
        public async Task<IActionResult> AssignmentTeacherToSchool(RequestView<AssignTeacherToSchoolView> view)
        {
            try
            {
                return Ok(await adminstrationService.AssignmentTeacherToSchool(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [EndpointSummary("TransformationId")]
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ConfirmStudentTransfer")]
        public async Task<IActionResult> ConfirmStudentTransfer(RequestView<long> view)
        {
            try
            {
                return Ok(await adminstrationService.ConfirmStudentTransfer(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}