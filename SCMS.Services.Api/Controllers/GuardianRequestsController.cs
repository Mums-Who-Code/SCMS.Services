// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SCMS.Services.Api.Models.Foundations.Guardians.Exceptions;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuardianRequestsController : RESTFulController
    {
        private readonly IStudentGuardianRequestOrchestrationService studentGuardianRequestOrchestrationService;

        public GuardianRequestsController(
            IStudentGuardianRequestOrchestrationService studentGuardianRequestOrchestrationService) =>
                this.studentGuardianRequestOrchestrationService = studentGuardianRequestOrchestrationService;

        [HttpPost]
        public async ValueTask<ActionResult<GuardianRequest>> PostGuardianRequest(GuardianRequest guardianRequest)
        {
            try
            {
                GuardianRequest addedGuardianRequest =
                    await this.studentGuardianRequestOrchestrationService
                        .AddStudentGuardianRequestAsync(guardianRequest);

                return Created(addedGuardianRequest);
            }
            catch (StudentGuardianRequestOrchestrationValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyValidationException validationException)
                when (validationException.InnerException
                    is InvalidStudentProcessingException
                    or InvalidStudentException
                    or NullGuardianRequestProcessingException
                    or InvalidGuardianRequestProcessingException
                    or NullGuardianException
                    or InvalidGuardianException
                    or NullStudentGuardianProcessingException
                    or InvalidStudentGuardianProcessingException
                    or NullStudentGuardianException
                    or InvalidStudentGuardianException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyValidationException validationException)
                when (validationException.InnerException is NotFoundStudentProcessingException)
            {
                return NotFound(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyValidationException validationException)
                when (validationException.InnerException is AlreadyExistsPrimaryStudentGuardianProcessingException)
            {
                return Conflict(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyValidationException validationException)
                when (validationException.InnerException is AlreadyExistsGuardianException
                        or AlreadyExistsStudentGuardianException)
            {
                return Conflict(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyValidationException validationException)
                when (validationException.InnerException is InvalidGuardianReferenceException
                        or InvalidStudentGuardianReferenceException)
            {
                return FailedDependency(validationException.InnerException);
            }
            catch (StudentGuardianRequestOrchestrationDependencyException dependencyException)
            {
                return InternalServerError(dependencyException);
            }
            catch (StudentGuardianRequestOrchestrationServiceException serviceException)
            {
                return InternalServerError(serviceException);
            }
        }
    }
}
