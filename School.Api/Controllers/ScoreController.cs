﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Dashboard;
using Application.Core.Views.Other;
using Application.Core.Views.Score;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace School.Api.Controllers
{
    public class ScoreController : BaseApiController
    {
        private readonly IScoreService scoreService;

        public ScoreController(IScoreService _scoreService)
        {
            scoreService = _scoreService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddScores")]
        public async Task<IActionResult> AddScores(RequestView<AddScoresView> view)
        {
            try
            {
                return Ok(await scoreService.AddScores(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddExamScores")]
        public async Task<IActionResult> AddExamScores(RequestView<AddScoresView> view)
        {
            try
            {
                return Ok(await scoreService.AddExamScores(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("IsClassExist")]
        public async Task<IActionResult> IsClassExist(RequestView<AddScoresView> view)
        {
            try
            {
                return Ok(await scoreService.IsClassExist(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ClassExamData), 200)]
        [EndpointSummary("TeacherClassesId")]
        [HttpPost("IsClassExamExist")]
        public async Task<IActionResult> IsClassExamExist(RequestView<long> view)
        {
            try
            {
                return Ok(await scoreService.IsClassExamExist(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetStudentForClassScore>), 200)]
        [HttpPost("GetStudentForClassScore")]
        public async Task<IActionResult> GetStudentForClassScore(RequestView<StudentForClassScoreView> view)
        {
            try
            {
                return Ok(await scoreService.GetStudentForClassScore(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<ScoreForPreAbsentData>), 200)]
        [HttpPost("ScoreForPreAbsent")]
        public async Task<IActionResult> ScoreForPreAbsent(RequestView<ScoreForPreAbsentView> view)
        {
            try
            {
                return Ok(await scoreService.ScoreForPreAbsent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditStudentScore")]
        public async Task<IActionResult> EditStudentScore(RequestView<EditStudentScoreView> view)
        {
            try
            {
                return Ok(await scoreService.EditStudentScore(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddEditScores")]
        public async Task<IActionResult> AddEditScores(RequestView<AddEditScoresView> view)
        {
            try
            {
                return Ok(await scoreService.AddEditScores(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}