// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SMCS.Services.Api.Models.Foundations.Students;
using SMCS.Services.Api.Models.Foundations.Students.Exceptions;
using SMCS.Services.Api.Services.Foundations.Students;

namespace SMCS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : RESTFulController
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService) =>
            this.studentService = studentService;

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student)
        {
            try
            {
                Student createdStudent =
                    await this.studentService.AddStudentAsync(student);

                return Created(createdStudent);
            }
            catch (StudentValidationException studentValidationException)
            {
                return BadRequest(studentValidationException.InnerException);
            }
            catch (StudentDependencyException studentDependencyException)
            {
                return InternalServerError(studentDependencyException);
            }
            catch (StudentDependencyValidationException studentDependencyValidationException)
                when (studentDependencyValidationException.InnerException is AlreadyExistsStudentException)
            {
                return Conflict(studentDependencyValidationException.InnerException);
            }
            catch (StudentServiceException studentServiceException)
            {
                return InternalServerError(studentServiceException);
            }
        }
    }
}
