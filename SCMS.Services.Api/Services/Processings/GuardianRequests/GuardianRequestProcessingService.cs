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
            Guardian mayBeGuardian = await RetrieveGuardianAsync(guardianRequest);

            return mayBeGuardian switch
            {
                null => await AddGuardianRequest(guardianRequest),
                _ => MapToGuardianRequest(mayBeGuardian, guardianRequest.StudentId)
            };
        }

        private async Task<Guardian> RetrieveGuardianAsync(GuardianRequest guardianRequest) =>
            await this.guardianService.RetrieveGuardianByIdAsync(guardianId: guardianRequest.Id);

        private async Task<GuardianRequest> AddGuardianRequest(GuardianRequest guardianRequest)
        {
            Guardian guardian = await AddGuardianAsync(guardianRequest);

            return MapToGuardianRequest(guardian, guardianRequest.StudentId);
        }

        private async ValueTask<Guardian> AddGuardianAsync(GuardianRequest guardianRequest)
        {
            Guardian guardian = MapToGuardian(guardianRequest);

            return await this.guardianService.AddGuardianAsync(guardian);
        }

        private Guardian MapToGuardian(GuardianRequest guardianRequest)
        {
            return new Guardian
            {
                Id = guardianRequest.Id,
                Title = (Title)guardianRequest.Title,
                FirstName = guardianRequest.FirstName,
                LastName = guardianRequest.LastName,
                EmailId = guardianRequest.EmailId,
                CountryCode = guardianRequest.CountryCode,
                ContactNumber = guardianRequest.ContactNumber,
                Occupation = guardianRequest.Occupation,
                CreatedDate = guardianRequest.CreatedDate,
                UpdatedDate = guardianRequest.UpdatedDate,
                CreatedBy = guardianRequest.CreatedBy,
                UpdatedBy = guardianRequest.UpdatedBy
            };
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
