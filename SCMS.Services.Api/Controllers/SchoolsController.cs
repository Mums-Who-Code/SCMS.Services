// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RESTFulSense.Controllers;
using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.Schools.Exceptions;
using SCMS.Services.Api.Services.Foundations.Schools;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : RESTFulController
    {
        private readonly ISchoolService schoolService;

        public SchoolsController(ISchoolService schoolService) =>
            this.schoolService = schoolService;

        [HttpPost]
        public async ValueTask<ActionResult<School>> PostSchoolAsync(School school)
        {
            try
            {
                School createdSchool =
                    await this.schoolService.AddSchoolAsync(school);

                return Created(createdSchool);
            }
            catch (SchoolValidationException schoolValidationException)
            {
                return BadRequest(schoolValidationException.InnerException);
            }
            catch (SchoolDependencyException schoolDependencyException)
            {
                return InternalServerError(schoolDependencyException);
            }
            catch (SchoolDependencyValidationException schoolDependencyValidationException)
                when (schoolDependencyValidationException.InnerException is AlreadyExistsSchoolException)
            {
                return Conflict(schoolDependencyValidationException.InnerException);
            }
            catch (SchoolDependencyValidationException schoolDependencyValidationException)
                when (schoolDependencyValidationException.InnerException is InvalidSchoolReferenceException)
            {
                return FailedDependency(schoolDependencyValidationException.InnerException);
            }
            catch (SchoolServiceException schoolServiceException)
            {
                return InternalServerError(schoolServiceException);
            }
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<School>> GetAllSchools()
        {
            try
            {
                IQueryable<School> schools =
                    this.schoolService.RetrieveAllSchools();

                return Ok(schools);
            }
            catch (SchoolDependencyException schoolDependencyException)
            {
                return InternalServerError(schoolDependencyException);
            }
            catch (SchoolServiceException schoolServiceException)
            {
                return InternalServerError(schoolServiceException);
            }
        }
    }
}
