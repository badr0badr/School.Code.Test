﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Student;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(RequestView<AddStudentView> view)
        {
            try
            {
                return Ok(await studentService.AddStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<ClassStudentsView>), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("GetClassStudents")]
        public async Task<IActionResult> GetClassStudents(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.GetClassStudents(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddStudentRange")]
        public async Task<IActionResult> AddStudentRange(RequestView<List<AddStudentView>> view)
        {
            try
            {
                return Ok(await studentService.AddStudentRange(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditStudentRange")]
        public async Task<IActionResult> EditStudentRange(RequestView<List<EditStudentView>> view)
        {
            try
            {
                return Ok(await studentService.EditStudentRange(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditStudentClass")]
        public async Task<IActionResult> EditStudentClass(RequestView<EditStudentView> view)
        {
            try
            {
                return Ok(await studentService.EditStudentClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.DeleteStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditStudent")]
        public async Task<IActionResult> EditStudent(RequestView<EditStudentView> view)
        {
            try
            {
                return Ok(await studentService.EditStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditStudentAbsentDays")]
        public async Task<IActionResult> EditStudentAbsentDays(RequestView<EditStudentAbsentDaysView> view)
        {
            try
            {
                return Ok(await studentService.EditStudentAbsentDays(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("DeleteClass")]
        public async Task<IActionResult> DeleteClass(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.DeleteClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("RestoreStudent")]
        public async Task<IActionResult> RestoreStudent(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.RestoreStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("StudentId")]
        [HttpPost("SoftDeleteStudent")]
        public async Task<IActionResult> SoftDeleteStudent(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.SoftDeleteStudent(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [HttpPost("GetDeletedStudents")]
        public async Task<IActionResult> GetDeletedStudents()
        {
            try
            {
                return Ok(await studentService.GetDeletedStudents());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SendStudentToAnotherSchool")]
        public async Task<IActionResult> SendStudentToAnotherSchool(RequestView<SendStudentToAnotherSchoolView> view)
        {
            try
            {
                return Ok(await studentService.SendStudentToAnotherSchool(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetTransformationsView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetStudentTransformationsReceiver")]
        public async Task<IActionResult> GetStudentTransformationsReceiver(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.GetStudentTransformationsReceiver(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetTransformationsView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetStudentTransformationsSender")]
        public async Task<IActionResult> GetStudentTransformationsSender(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.GetStudentTransformationsSender(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AcceptStudentTransformation")]
        public async Task<IActionResult> AcceptStudentTransformation(RequestView<AcceptStudentTransformationView> view)
        {
            try
            {
                return Ok(await studentService.AcceptStudentTransformation(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(GetAllStudentStaticsSchoolData), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetAllStudentStatics")]
        public async Task<IActionResult> GetAllStudentStatics(RequestView<long> view)
        {
            try
            {
                return Ok(await studentService.GetAllStudentStatics(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<UploadedStudentNames>), 200)]
        [HttpPost("UploadStudentNames")]
        [RequestSizeLimit(10 * 1024 * 1024)] // Limit to 10 MB
        public async Task<IActionResult> UploadStudentNames(IFormFile view)
        {
            try
            {
                return Ok(await studentService.UploadStudentNames(view));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("UploadStudentCodes")]
        [RequestSizeLimit(10 * 1024 * 1024)] // Limit to 10 MB
        public async Task<IActionResult> UploadStudentCodes(IFormFile view)
        {
            try
            {
                return Ok(await studentService.UploadStudentCodes(view));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}