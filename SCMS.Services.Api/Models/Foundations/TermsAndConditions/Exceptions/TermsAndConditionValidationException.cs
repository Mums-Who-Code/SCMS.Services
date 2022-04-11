// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class TermsAndConditionValidationException : Xeption
    {
        public TermsAndConditionValidationException(Xeption innerException)
            : base(message: "Terms And Condition validation error occured, fix the error and try again.",
                    innerException)
        { }
    }
}
