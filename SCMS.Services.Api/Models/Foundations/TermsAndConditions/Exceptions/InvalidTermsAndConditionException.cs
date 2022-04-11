// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class InvalidTermsAndConditionException : Xeption
    {
        public InvalidTermsAndConditionException()
            : base(message: "Invalid terms and condition, fix the errors and try again.")
        { }
    }
}
