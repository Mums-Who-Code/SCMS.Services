// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;

namespace SCMS.Services.Api.Services.Foundations.TermsAndConditions
{
    public interface ITermsAndConditionService
    {
        ValueTask<TermsAndCondition> AddTermsAndConditionAsync(TermsAndCondition termsAndCondition);
    }
}
