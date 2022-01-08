// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Foundations.Guardians;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public class GuardianRequestProcessingService : IGuardianRequestProcessingService
    {
        private readonly IGuardianService guardianService;
        private readonly ILoggingBroker loggingBroker;

        public GuardianRequestProcessingService(
            IGuardianService guardianService,
            ILoggingBroker loggingBroker)
        {
            this.guardianService = guardianService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<GuardianRequest> EnsureGuardianRequestExists(GuardianRequest guardianRequest)
        {
            Guardian returningGuardian = await this.guardianService
                .RetrieveGuardianByIdAsync(guardianId: guardianRequest.Id);

            return MapToGuardianRequest(returningGuardian, guardianRequest.StudentId);
        }

        private GuardianRequest MapToGuardianRequest(Guardian guardian, Guid studentId)
        {
            return new GuardianRequest
            {
                Id = guardian.Id,
                Title = (GuardianRequestTitle)guardian.Title,
                FirstName = guardian.FirstName,
                LastName = guardian.LastName,
                EmailId = guardian.EmailId,
                CountryCode = guardian.CountryCode,
                ContactNumber = guardian.ContactNumber,
                Occupation = guardian.Occupation,
                StudentId = studentId,
                CreatedDate = guardian.CreatedDate,
                UpdatedDate = guardian.UpdatedDate,
                CreatedBy = guardian.CreatedBy,
                UpdatedBy = guardian.UpdatedBy
            };
        }
    }
}
