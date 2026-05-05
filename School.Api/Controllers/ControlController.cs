﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Auth;
using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class ControlController : BaseApiController
    {
        private readonly IControlService controlService;
        public ControlController(IControlService _controlService)
        {
            controlService = _controlService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddToControl")]
        public async Task<IActionResult> AddToControl(RequestView<AddToControlView> view)
        {
            try
            {
                return Ok(await controlService.AddToControl(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("DeleteFromControl")]
        public async Task<IActionResult> DeleteFromControl(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.DeleteFromControl(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<ControlTeachersData>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetControlTeachers")]
        public async Task<IActionResult> GetControlTeachers(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.GetControlTeachers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdStringNameView>), 200)]
        [HttpPost("GetControlTypes")]
        public async Task<IActionResult> GetControlTypes()
        {
            try
            {
                return Ok(await controlService.GetControlTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("AddPlaceNumbers")]
        public async Task<IActionResult> AddPlaceNumbers(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.AddPlaceNumbers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("DeletePlaceNumbers")]
        public async Task<IActionResult> DeletePlaceNumbers(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.DeletePlaceNumbers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("GetStudentCountInExamHall")]
        public async Task<IActionResult> GetStudentCountInExamHall(RequestView<StudentExamHallView> view)
        {
            try
            {
                return Ok(await controlService.GetStudentCountInExamHall(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddStudentInExamHall")]
        public async Task<IActionResult> AddStudentInExamHall(RequestView<StudentExamHallView> view)
        {
            try
            {
                return Ok(await controlService.AddStudentInExamHall(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("DeleteStudentFromExamHall")]
        public async Task<IActionResult> DeleteStudentFromExamHall(RequestView<DeleteStudentExamView> view)
        {
            try
            {
                return Ok(await controlService.DeleteStudentFromExamHall(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("AddSecriteCodes")]
        public async Task<IActionResult> AddSecriteCodes(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.AddSecriteCodes(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        //[ProducesResponseType(typeof(ErrorResponce), 200)]
        //[EndpointSummary("SchoolId")]
        //[HttpPost("DeleteSecriteCodes")]
        //public async Task<IActionResult> DeleteSecriteCodes(RequestView<long> view)
        //{
        //    try
        //    {
        //        return Ok(await controlService.DeleteSecriteCodes(view.Request));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
        //    }
        //}
        [ProducesResponseType(typeof(StudentsPlaceNumbers), 200)]
        [HttpPost("GetPlaceNumbers")]
        public async Task<IActionResult> GetPlaceNumbers(RequestView<GetPlaceNumbersView> view)
        {
            try
            {
                return Ok(await controlService.GetPlaceNumbers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("PreparingExam")]
        public async Task<IActionResult> PreparingExam(RequestView<PreparingFinalExamView> view)
        {
            try
            {
                return Ok(await controlService.PreparingExam(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("DeletePreparingExam")]
        public async Task<IActionResult> DeletePreparingExam(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.DeletePreparingExam(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<MerrorPdfData>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("MerrorData")]
        public async Task<IActionResult> MerrorData(RequestView<MerrorDataView> view)
        {
            try
            {
                return Ok(await controlService.MerrorData(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<ExamTempltesModelData>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetExamTempltesModel")]
        public async Task<IActionResult> GetExamTempltesModel(RequestView<long> view)
        {
            try
            {
                return Ok(await controlService.GetExamTempltesModel(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}