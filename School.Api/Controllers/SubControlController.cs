﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Control;
using Application.Core.Views.Other;
using Application.Core.Views.Reports;
using Application.Core.Views.Score;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace School.Api.Controllers
{
    public class SubControlController : BaseApiController
    {
        private readonly ISubControlService subControlService;
        public SubControlController(ISubControlService _subControlService)
        {
            subControlService = _subControlService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SaveStudentsForAppliedExam")]
        public async Task<IActionResult> SaveStudentsForAppliedExam(RequestView<SaveStudentsForAppliedExamView> view)
        {
            try
            {
                return Ok(await subControlService.SaveStudentsForAppliedExam(view.Token,view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentsForAppliedExamData>), 200)]
        [HttpPost("GetStudentsForAppliedExam")]
        public async Task<IActionResult> GetStudentsForAppliedExam(RequestView<StudentsForAppliedExamView> view)
        {
            try
            {
                return Ok(await subControlService.GetStudentsForAppliedExam(view.Token, view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<string>), 200)]
        [HttpPost("GetAllFileNamesInFolder")]
        public async Task<IActionResult> GetAllFileNamesInFolder(RequestView<string> view)
        {
            try
            {
                return Ok(await subControlService.GetAllFileNamesInFolder(view.Token));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<HallSammryData>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("HallSummryDatas")]
        public async Task<IActionResult> HallSummryDatas(RequestView<long> view)
        {
            try
            {
                return Ok(await subControlService.HallSummryDatas(view.Token,view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<HallSammryData>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetStudentTackits")]
        public async Task<IActionResult> GetStudentTackits(RequestView<long> view)
        {
            try
            {
                return Ok(await subControlService.GetStudentTackits(view.Token, view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("Code")]
        [HttpPost("CheakCode")]
        public async Task<IActionResult> CheakCode(RequestView<long> view)
        {
            try
            {
                return Ok(await subControlService.CheakCode(view.Token,view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("Code")]
        [HttpPost("ViewStudentData")]
        public async Task<IActionResult> ViewStudentData(RequestView<long> view)
        {
            try
            {
                return Ok(await subControlService.ViewStudentData(view.Token,view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddExamResult")]
        public async Task<IActionResult> AddExamResult(RequestView<AddExamResultView> view)
        {
            try
            {
                return Ok(await subControlService.AddExamResult(view.Token,view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentExamAbsentData>), 200)]
        [HttpPost("GetExamAbsent")]
        public async Task<IActionResult> GetExamAbsent(RequestView<GetExamAbsentView> view)
        {
            try
            {
                return Ok(await subControlService.GetExamAbsent(view.Token, view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentExamAbsentData>), 200)]
        [HttpPost("SaveExamAbsent")]
        public async Task<IActionResult> SaveExamAbsent(RequestView<SaveExamAbsentView> view)
        {
            try
            {
                return Ok(await subControlService.SaveExamAbsent(view.Token, view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(HandlingExamPapersToRateData), 200)]
        [HttpPost("HandlingExamPapersToRate")]
        public async Task<IActionResult> HandlingExamPapersToRate(RequestView<HandlingExamPapersToRateView> view)
        {
            try
            {
                return Ok(await subControlService.HandlingExamPapersToRate(view.Token, view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }


















        [HttpGet("Download/{FileName}")]
        public IActionResult DownloadFile(string FileName)
        {
            try
            {
                var FolderName = Path.Combine("wwwroot", "Resources", "ControlPDFs");
                var FilePath = Path.Combine(Directory.GetCurrentDirectory(), FolderName, FileName);
                if (!System.IO.File.Exists(FilePath))
                {
                    return NotFound(new ApiError(StatusCodes.Status404NotFound, "بالرجاء اعادة فتح التقرير مره اخري"));
                }
                string MimeType = GetContentType(FilePath);
                var FileBytes = System.IO.File.ReadAllBytes(FilePath);
                var file = File(FileBytes, MimeType, FileName);
                return file;
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        private string GetContentType(string Path)
        {
            var Provider = new FileExtensionContentTypeProvider();
            if (!Provider.TryGetContentType(Path, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}