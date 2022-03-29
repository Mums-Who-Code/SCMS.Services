// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Processings.GuardianRequests;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public interface IStudentGuardianRequestOrchestrationService
    {
        ValueTask<GuardianRequest> AddStudentGuardianRequestAsync(GuardianRequest guardianRequest);
    }
}
