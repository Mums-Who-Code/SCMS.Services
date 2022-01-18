// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Processings.GuardianRequests;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public interface IStudentGuardianRequestOrchestrationService
    {
        ValueTask<GuardianRequest> AddStudentGuardianRequestAsync(GuardianRequest guardianRequest);
    }
}
