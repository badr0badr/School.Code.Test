﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class DirectorateController : BaseApiController
    {
        private readonly IDirectorateService directorateService;
        public DirectorateController(IDirectorateService _directorateService)
        {
            directorateService = _directorateService;
        }

        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddDirectorateUser")]
        public async Task<IActionResult> AddDirectorateUser(RequestView<AddAgencyUserView> view)
        {
            try
            {
                return Ok(await directorateService.AddDirectorateUser(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddAgency")]
        public async Task<IActionResult> AddAgency(RequestView<AddAgencyView> view)
        {
            try
            {
                return Ok(await directorateService.AddAgency(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}