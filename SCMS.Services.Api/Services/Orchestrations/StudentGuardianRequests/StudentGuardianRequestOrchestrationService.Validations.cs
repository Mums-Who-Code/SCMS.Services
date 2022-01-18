// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationService
    {
        private void ValidateStudentGuardianRequest(GuardianRequest guardianRequest)
        {
            if (guardianRequest == null)
            {
                throw new NullStudentGuardianRequestOrchestrationException();
            }
        }
    }
}
