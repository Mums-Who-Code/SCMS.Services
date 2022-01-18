// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using SCMS.Services.Api.Services.Processings.Students;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public class StudentGuardianRequestOrchestrationService : IStudentGuardianRequestOrchestrationService
    {
        private readonly IStudentProcessingService studentProcessingService;
        private readonly IGuardianRequestProcessingService guardianRequestProcessingService;
        private readonly IStudentGuardianProcessingService studentGuardianProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public StudentGuardianRequestOrchestrationService(
            IStudentProcessingService studentProcessingService,
            IGuardianRequestProcessingService guardianRequestProcessingService,
            IStudentGuardianProcessingService studentGuardianProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.studentProcessingService = studentProcessingService;
            this.guardianRequestProcessingService = guardianRequestProcessingService;
            this.studentGuardianProcessingService = studentGuardianProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<GuardianRequest> AddStudentGuardianRequestAsync(GuardianRequest guardianRequest)
        {
            await this.studentProcessingService.VerifyStudentExistsAsync(guardianRequest.StudentId);

            GuardianRequest addedGuardianRequest =
                await this.guardianRequestProcessingService.EnsureGuardianRequestExists(guardianRequest);

            StudentGuardian addedStudentGuardian =
                await AddStudentGuardianAsync(guardianRequest);

            return MapToGuardianRequest(addedGuardianRequest, addedStudentGuardian);
        }

        private async ValueTask<StudentGuardian> AddStudentGuardianAsync(GuardianRequest guardianRequest)
        {
            StudentGuardian studentGuardian = MapToStudentGuardian(guardianRequest);

            return await this.studentGuardianProcessingService.AddStudentGuardianAsync(studentGuardian);
        }

        private StudentGuardian MapToStudentGuardian(GuardianRequest guardianRequest)
        {
            return new StudentGuardian
            {
                StudentId = guardianRequest.StudentId,
                GuardianId = guardianRequest.Id,
                Relation = (Relationship)guardianRequest.Relationship,
                Level = (ContactLevel)guardianRequest.ContactLevel,
                CreatedDate = guardianRequest.CreatedDate,
                UpdatedDate = guardianRequest.UpdatedDate,
                CreatedBy = guardianRequest.CreatedBy,
                UpdatedBy = guardianRequest.UpdatedBy
            };
        }

        private GuardianRequest MapToGuardianRequest(
            GuardianRequest addedGuardianRequest,
            StudentGuardian addedStudentGuardian)
        {
            return new GuardianRequest
            {
                Id = addedGuardianRequest.Id,
                Title = addedGuardianRequest.Title,
                FirstName = addedGuardianRequest.FirstName,
                LastName = addedGuardianRequest.LastName,
                EmailId = addedGuardianRequest.EmailId,
                CountryCode = addedGuardianRequest.CountryCode,
                ContactNumber = addedGuardianRequest.ContactNumber,
                Occupation = addedGuardianRequest.Occupation,
                ContactLevel = (GuardianRequestContactLevel)addedStudentGuardian.Level,
                Relationship = (GuardianRequestRelationship)addedStudentGuardian.Relation,
                StudentId = addedStudentGuardian.StudentId,
                CreatedDate = addedGuardianRequest.CreatedDate,
                UpdatedDate = addedGuardianRequest.UpdatedDate,
                CreatedBy = addedGuardianRequest.CreatedBy,
                UpdatedBy = addedGuardianRequest.UpdatedBy
            };
        }
    }
}
