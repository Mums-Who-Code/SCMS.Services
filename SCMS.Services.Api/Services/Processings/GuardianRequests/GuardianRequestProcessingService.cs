// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
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

        public ValueTask<GuardianRequest> EnsureGuardianRequestExists(GuardianRequest guardianRequest) =>
            throw new NotImplementedException();
    }
}
