// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
using SMCS.Services.Api.Services.Foundations.StudentSchools;

namespace SMCS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentSchoolsController : RESTFulController
    {
        private readonly IStudentSchoolService studentSchoolService;

        public StudentSchoolsController(IStudentSchoolService studentSchoolService) =>
            this.studentSchoolService = studentSchoolService;

        [HttpPost]
        public async ValueTask<ActionResult<StudentSchool>> PostStudentSchoolAsync(StudentSchool studentSchool)
        {
            try
            {
                StudentSchool createdStudentSchool =
                    await this.studentSchoolService.AddStudentSchoolAsync(studentSchool);

                return Created(createdStudentSchool);
            }
            catch (StudentSchoolValidationException studentSchoolValidationException)
            {
                return BadRequest(studentSchoolValidationException.InnerException);
            }
            catch (StudentSchoolDependencyException studentSchoolDependencyException)
            {
                return Problem(studentSchoolDependencyException.Message);
            }
            catch (StudentSchoolDependencyValidationException studentSchoolDependencyValidationException)
                when (studentSchoolDependencyValidationException.InnerException is AlreadyExistsStudentSchoolException)
            {
                return Conflict(studentSchoolDependencyValidationException.InnerException);
            }
            catch (StudentSchoolDependencyValidationException studentSchoolDependencyValidationException)
                when (studentSchoolDependencyValidationException.InnerException is InvalidStudentSchoolReferenceException)
            {
                return FailedDependency(studentSchoolDependencyValidationException.InnerException);
            }
            catch (StudentSchoolDependencyValidationException studentSchoolDependencyValidationException)
                when (studentSchoolDependencyValidationException.InnerException is RepeatedStudentSchoolException)
            {
                return FailedDependency(studentSchoolDependencyValidationException.InnerException);
            }
            catch (StudentSchoolServiceException studentSchoolServiceException)
            {
                return InternalServerError(studentSchoolServiceException);
            }
        }
    }
}
