﻿using Application.Core.Entities;
using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Control;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Application.Core.Views.TekStatics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class TekStaticsController : BaseApiController
    {
        private readonly ITekStaticsService tekStaticsService;
        public TekStaticsController(ITekStaticsService _tekStaticsService)
        {
            tekStaticsService = _tekStaticsService;
        }
        [ProducesResponseType(typeof(ClassSheetPdf), 200)]
        [HttpPost("GetClassSheets")]
        public async Task<IActionResult> GetClassSheets(RequestView<ClassSheetPdfView> view)
        {
            try
            {
                return Ok(await tekStaticsService.GetClassSheets(view.Request));
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
                return Ok(await tekStaticsService.Get5Behavior(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MonitorGradesDetails), 200)]
        [HttpPost("MonitorGrades")]
        public async Task<IActionResult> MonitorGrades(RequestView<MonitorGradesView> view)
        {
            try
            {
                return Ok(await tekStaticsService.MonitorGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MonitorGradesDataDetails), 200)]
        [HttpPost("MonitorGradesData")]
        public async Task<IActionResult> MonitorGradesData(RequestView<StudentForClassScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.MonitorGradesData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("RestMonitorGrades")]
        public async Task<IActionResult> RestMonitorGrades(RequestView<StudentForClassScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.RestMonitorGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ConfirmMonitorGrades")]
        public async Task<IActionResult> ConfirmMonitorGrades(RequestView<StudentForClassScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.ConfirmMonitorGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MonitorGradesDetails), 200)]
        [HttpPost("GetMonitorExamGrades")]
        public async Task<IActionResult> GetMonitorExamGrades(RequestView<StudentForExamGradeView> view)
        {
            try
            {
                return Ok(await tekStaticsService.GetMonitorExamGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(MonitotExamGradesDataDetails), 200)]
        [HttpPost("MonitorExamGradesData")]
        public async Task<IActionResult> MonitorExamGradesData(RequestView<StudentForExamScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.MonitorExamGradesData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("RestMonitorExamGrades")]
        public async Task<IActionResult> RestMonitorExamGrades(RequestView<StudentForExamScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.RestMonitorExamGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ConfirmMonitorExamGrades")]
        public async Task<IActionResult> ConfirmMonitorExamGrades(RequestView<StudentForExamScoreView> view)
        {
            try
            {
                return Ok(await tekStaticsService.ConfirmMonitorExamGrades(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("TestConfirm")]
        public async Task<IActionResult> TestConfirm(RequestView<long> view)
        {
            try
            {
                return Ok(await tekStaticsService.TestConfirm(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}