// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class TermsAndConditionDependencyValidationException : Xeption
    {
        public TermsAndConditionDependencyValidationException(Xeption innerException)
            : base(message: "Terms and condition dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
