// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Agreements;

namespace SCMS.Services.Api.Services.Foundations.Agreements
{
    public interface IAgreementService
    {
        ValueTask<Agreement> AddAgreementAsync(Agreement agreement);
    }
}
