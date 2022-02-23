﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Foundations.Guardians;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public partial class GuardianRequestProcessingService : IGuardianRequestProcessingService
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

        public ValueTask<GuardianRequest> EnsureGuardianRequestExistsAsync(GuardianRequest guardianRequest) =>
        TryCatch(async () =>
        {
            ValidateGuardianRequest(guardianRequest);
            Guardian mayBeGuardian = await RetrieveGuardianAsync(guardianRequest);

            return mayBeGuardian switch
            {
                null => await AddGuardianRequest(guardianRequest),
                _ => MapToGuardianRequest(mayBeGuardian, guardianRequest)
            };
        });

        private async Task<Guardian> RetrieveGuardianAsync(GuardianRequest guardianRequest) =>
            await this.guardianService.RetrieveGuardianByIdAsync(guardianId: guardianRequest.Id);

        private async Task<GuardianRequest> AddGuardianRequest(GuardianRequest guardianRequest)
        {
            Guardian guardian = await AddGuardianAsync(guardianRequest);

            return MapToGuardianRequest(guardian, guardianRequest);
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
                Email = guardianRequest.Email,
                CountryCode = guardianRequest.CountryCode,
                ContactNumber = guardianRequest.ContactNumber,
                Occupation = guardianRequest.Occupation,
                CreatedDate = guardianRequest.CreatedDate,
                UpdatedDate = guardianRequest.UpdatedDate,
                CreatedBy = guardianRequest.CreatedBy,
                UpdatedBy = guardianRequest.UpdatedBy
            };
        }

        private GuardianRequest MapToGuardianRequest(Guardian guardian, GuardianRequest guardianRequest)
        {
            return new GuardianRequest
            {
                Id = guardian.Id,
                Title = (GuardianRequestTitle)guardian.Title,
                FirstName = guardian.FirstName,
                LastName = guardian.LastName,
                Email = guardian.Email,
                CountryCode = guardian.CountryCode,
                ContactNumber = guardian.ContactNumber,
                Occupation = guardian.Occupation,
                ContactLevel = guardianRequest.ContactLevel,
                Relationship = guardianRequest.Relationship,
                StudentId = guardianRequest.StudentId,
                CreatedDate = guardian.CreatedDate,
                UpdatedDate = guardian.UpdatedDate,
                CreatedBy = guardian.CreatedBy,
                UpdatedBy = guardian.UpdatedBy
            };
        }
    }
}
