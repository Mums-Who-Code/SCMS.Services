// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentGuardiansController : RESTFulController
    {
        private readonly IStudentGuardianProcessingService studentGuardianProcessingService;

        public StudentGuardiansController(IStudentGuardianProcessingService studentGuardianProcessingService) =>
            this.studentGuardianProcessingService = studentGuardianProcessingService;

        [HttpPost]
        public async ValueTask<ActionResult<StudentGuardian>> PostStudentGuardianAsync(StudentGuardian
            studentGuardian)
        {
            try
            {
                StudentGuardian createdStudentGuardian =
                    await this.studentGuardianProcessingService.AddStudentGuardianAsync(studentGuardian);

                return Created(createdStudentGuardian);
            }
            catch (StudentGuardianProcessingValidationException studentGuardianProcessingValidationException)
            when (studentGuardianProcessingValidationException.InnerException
                is AlreadyExistsPrimaryStudentGuardianProcessingException)
            {
                return Forbidden(studentGuardianProcessingValidationException.InnerException);
            }
            catch (StudentGuardianProcessingValidationException studentGuardianProcessingValidationException)
            {
                return BadRequest(studentGuardianProcessingValidationException.InnerException);
            }
            catch (StudentGuardianProcessingDependencyException studentGuardianProcessingDependencyException)
            {
                return InternalServerError(studentGuardianProcessingDependencyException);
            }
            catch (StudentGuardianProcessingDependencyValidationException
                studentGuardianProcessingDependencyValidationException)
                when (studentGuardianProcessingDependencyValidationException.InnerException
                    is NullStudentGuardianException
                    or InvalidStudentGuardianException)
            {
                return BadRequest(studentGuardianProcessingDependencyValidationException.InnerException);
            }
            catch (StudentGuardianProcessingDependencyValidationException
                studentGuardianProcessingDependencyValidationException)
                when (studentGuardianProcessingDependencyValidationException.InnerException
                    is AlreadyExistsStudentGuardianException)
            {
                return Conflict(studentGuardianProcessingDependencyValidationException.InnerException);
            }
            catch (StudentGuardianProcessingDependencyValidationException
                studentGuardianProcessingDependencyValidationException)
                when (studentGuardianProcessingDependencyValidationException.InnerException
                    is InvalidStudentGuardianReferenceException)
            {
                return FailedDependency(studentGuardianProcessingDependencyValidationException.InnerException);
            }
            catch (StudentGuardianProcessingServiceException studentGuardianProcessingServiceException)
            {
                return InternalServerError(studentGuardianProcessingServiceException);
            }
        }
    }
}
