// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Processings.GuardianRequests;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public interface IGuardianRequestProcessingService
    {
        ValueTask<GuardianRequest> EnsureGuardianRequestExists(GuardianRequest guardianRequest);
    }
}
