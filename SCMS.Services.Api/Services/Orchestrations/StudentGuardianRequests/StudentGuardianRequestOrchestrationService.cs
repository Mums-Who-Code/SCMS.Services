// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
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
        private readonly IStudentGuardianProcessingService studentGuardianRequestProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public StudentGuardianRequestOrchestrationService(
            IStudentProcessingService studentProcessingService,
            IGuardianRequestProcessingService guardianRequestProcessingService,
            IStudentGuardianProcessingService studentGuardianRequestProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.studentProcessingService = studentProcessingService;
            this.guardianRequestProcessingService = guardianRequestProcessingService;
            this.studentGuardianRequestProcessingService = studentGuardianRequestProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<GuardianRequest> AddStudentGuardianRequestAsync(GuardianRequest guardianRequest) =>
            throw new NotImplementedException();
    }
}
