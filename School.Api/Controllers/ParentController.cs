﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Parent;
using Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class ParentController : BaseApiController
    {
        private readonly IParentService parentService;

        public ParentController(IParentService _parentService)
        {
            parentService = _parentService;
        }
        [ProducesResponseType(typeof(StudentProfile), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("GetStudentProfile")]
        public async Task<IActionResult> GetStudentProfile(RequestView<long> view)
        {
            try
            {
                return Ok(await parentService.GetStudentProfile(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentPunchmentsData>), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("GetStudentPunchments")]
        public async Task<IActionResult> GetStudentPunchments(RequestView<long> view)
        {
            try
            {
                return Ok(await parentService.GetStudentPunchments(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("PunchmentId")]
        [HttpPost("ConfirmStudentPunchments")]
        public async Task<IActionResult> ConfirmStudentPunchments(RequestView<long> view)
        {
            try
            {
                return Ok(await parentService.ConfirmStudentPunchments(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentComplentData>), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("GetStudentComplents")]
        public async Task<IActionResult> GetStudentComplents(RequestView<long> view)
        {
            try
            {
                return Ok(await parentService.GetStudentComplents(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentComplentData>), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("GetStudentAvrageView")]
        public async Task<IActionResult> GetStudentAvrageView(RequestView<long> view)
        {
            try
            {
                return Ok(await parentService.GetStudentAvrageView(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SendComplent")]
        public async Task<IActionResult> SendComplent(RequestView<SendComplentView> view)
        {
            try
            {
                return Ok(await parentService.SendComplent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}