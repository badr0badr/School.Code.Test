﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Auth;
using Application.Core.Views.Other;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }
        [ProducesResponseType(typeof(TeacherData), 200)]
        [HttpPost("TeacherLogin")]
        public async Task<IActionResult> TeacherLogin(RequestView<LoginView> view)
        {
            try
            {
                return Ok(await authService.TeacherLogin(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(TeacherData), 200)]
        [HttpPost("ControlTeacherLogin")]
        public async Task<IActionResult> ControlTeacherLogin(RequestView<LoginView> view)
        {
            try
            {
                return Ok(await authService.ControlTeacherLogin(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }


        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin(RequestView<SigninView> view)
        {
            try
            {
                return Ok(await authService.Signin(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }


        [ProducesResponseType(typeof(TeacherData), 200)]
        [EndpointSummary("Token")]
        [HttpPost("TeacherDataByToken")]
        public async Task<IActionResult> TeacherDataByToken(RequestView<string> view)
        {
            try
            {
                return Ok(await authService.TeacherDataByToken(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }


        [ProducesResponseType(typeof(PerantData), 200)]
        [EndpointSummary("Code")]
        [HttpPost("PerantLogin")]
        public async Task<IActionResult> PerantLogin(RequestView<long> view)
        {
            try
            {
                return Ok(await authService.PerantLogin(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }


        [ProducesResponseType(typeof(PerantData), 200)]
        [EndpointSummary("Token")]
        [HttpPost("PerantDataByToken")]
        public async Task<IActionResult> PerantDataByToken(RequestView<string> view)
        {
            try
            {
                return Ok(await authService.PerantDataByToken(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(PerantData), 200)]
        [EndpointSummary("Token")]
        [HttpPost("ControlTeacherDataByToken")]
        public async Task<IActionResult> ControlTeacherDataByToken(RequestView<string> view)
        {
            try
            {
                return Ok(await authService.ControlTeacherDataByToken(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}