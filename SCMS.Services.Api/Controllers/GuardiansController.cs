// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Guardians.Exceptions;
using SCMS.Services.Api.Services.Foundations.Guardians;

namespace SCMS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuardiansController : RESTFulController
    {
        private readonly IGuardianService guardianService;

        public GuardiansController(IGuardianService guardianService) =>
            this.guardianService = guardianService;

        [HttpPost]
        public async ValueTask<ActionResult<Guardian>> PostGuardianAsync(Guardian guardian)
        {
            try
            {
                Guardian createdGuardian =
                    await this.guardianService.AddGuardianAsync(guardian);

                return Created(createdGuardian);
            }
            catch (GuardianValidationException guardianValidationException)
            {
                return BadRequest(guardianValidationException.InnerException);
            }
            catch (GuardianDependencyException guardianDependencyException)
            {
                return InternalServerError(guardianDependencyException);
            }
            catch (GuardianDependencyValidationException guardianDependencyValidationException)
                when (guardianDependencyValidationException.InnerException is AlreadyExistsGuardianException)
            {
                return Conflict(guardianDependencyValidationException.InnerException);
            }
            catch (GuardianDependencyValidationException guardianDependencyValidationException)
                when (guardianDependencyValidationException.InnerException is InvalidGuardianReferenceException)
            {
                return FailedDependency(guardianDependencyValidationException.InnerException);
            }
            catch (GuardianServiceException guardianServiceException)
            {
                return InternalServerError(guardianServiceException);
            }
        }
    }
}
