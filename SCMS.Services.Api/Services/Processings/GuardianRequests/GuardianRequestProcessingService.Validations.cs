// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public partial class GuardianRequestProcessingService : IGuardianRequestProcessingService
    {
        private void ValidateGuardianRequest(GuardianRequest guardianRequest)
        {
            if (guardianRequest == null)
            {
                throw new NullGuardianRequestProcessingException();
            }
        }
    }
}
