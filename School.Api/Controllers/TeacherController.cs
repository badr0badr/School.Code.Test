﻿using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Views.Other;
using Application.Core.Views.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService _teacherService)
        {
            teacherService = _teacherService;
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(RequestView<AddTeacherView> view)
        {
            try
            {
                return Ok(await teacherService.AddTeacher(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<string>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetTeacherSubjects")]
        public async Task<IActionResult> GetTeacherSubjects(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetTeacherSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<StudentForClassView>), 200)]
        [EndpointSummary("ClassId")]
        [HttpPost("GetStudentForClass")]
        public async Task<IActionResult> GetStudentForClass(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetStudentForClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.DeleteTeacher(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("EditTeacher")]
        public async Task<IActionResult> EditTeacher(RequestView<EditTeacherView> view)
        {
            try
            {
                return Ok(await teacherService.EditTeacher(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AssignTeacherToClass")]
        public async Task<IActionResult> AssignTeacherToClass(RequestView<TeacherToClassView> view)
        {
            try
            {
                return Ok(await teacherService.AssignTeacherToClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetTeachersView>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetTeachers")]
        public async Task<IActionResult> GetTeachers(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetTeachers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddClass")]
        public async Task<IActionResult> AddClass(RequestView<AddClassView> view)
        {
            try
            {
                return Ok(await teacherService.AddClass(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("SubjectName")]
        public async Task<IActionResult> AddSubject(RequestView<string> view)
        {
            try
            {
                return Ok(await teacherService.AddSubject(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetAssignTeacherView>), 200)]
        [HttpPost("GetAssignTeacher")]
        public async Task<IActionResult> GetAssignTeacher(RequestView<AssignTeacherView> view)
        {
            try
            {
                return Ok(await teacherService.GetAssignTeacher(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [HttpPost("GetUnAssignClasses")]
        public async Task<IActionResult> GetUnAssignClasses(RequestView<AssignTeacherView> view)
        {
            try
            {
                return Ok(await teacherService.GetUnAssignClasses(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("AssignId")]
        [HttpPost("DeleteAssign")]
        public async Task<IActionResult> DeleteAssign(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.DeleteAssign(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddPA")]
        public async Task<IActionResult> AddPA(RequestView<AddPAView> view)
        {
            try
            {
                return Ok(await teacherService.AddPA(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetPA")]
        public async Task<IActionResult> GetPA(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetPA(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("PAId")]
        [HttpPost("DeletePA")]
        public async Task<IActionResult> DeletePA(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.DeletePA(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("CheckManagement")]
        public async Task<IActionResult> CheckManagement(RequestView<CheckManagementView> view)
        {
            try
            {
                return Ok(await teacherService.CheckManagement(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.ResetPassword(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<GetTeachersView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetAllWorkers(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetSuperSubjects")]
        public async Task<IActionResult> GetSuperSubjects(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetSuperSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetSupervisors")]
        public async Task<IActionResult> GetSupervisors(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetSupervisors(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("SpeareSubjects")]
        public async Task<IActionResult> SpeareSubjects(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.SpearSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(List<IdNumberNameView>), 200)]
        [EndpointSummary("TeacherId")]
        [HttpPost("GetNonSupervisors")]
        public async Task<IActionResult> GetNonSupervisors(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetNonSupervisors(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ChangeSupervisor")]
        public async Task<IActionResult> ChangeSupervisor(RequestView<ChangeSupervisorView> view)
        {
            try
            {
                return Ok(await teacherService.ChangeSupervisor(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("AddSuperSubjects")]
        public async Task<IActionResult> AddSuperSubjects(RequestView<AddSuperSubjectsView> view)
        {
            try
            {
                return Ok(await teacherService.AddSuperSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SuperSubjectId")]
        [HttpPost("DeleteSuperSubjects")]
        public async Task<IActionResult> DeleteSuperSubjects(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.DeleteSuperSubjects(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [EndpointSummary("SchoolId")]
        [HttpPost("GetQualityControlTeacherName")]
        public async Task<IActionResult> GetQualityControlTeacherName(RequestView<long> view)
        {
            try
            {
                return Ok(await teacherService.GetQualityControlTeacherName(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
        [ProducesResponseType(typeof(ErrorResponce), 200)]
        [HttpPost("ChangeQualityControl")]
        public async Task<IActionResult> ChangeQualityControl(RequestView<ChangeQuiltyControlView> view)
        {
            try
            {
                return Ok(await teacherService.ChangeQualityControl(view.Request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(StatusCodes.Status404NotFound, ex.Message));
            }
        }
    }
}