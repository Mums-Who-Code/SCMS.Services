// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionService
    {
        private static void ValidateInput(TermsAndCondition termsAndCondition)
        {
            if (termsAndCondition == null)
            {
                throw new NullTermsAndConditionException();
            }
        }
    }
}
