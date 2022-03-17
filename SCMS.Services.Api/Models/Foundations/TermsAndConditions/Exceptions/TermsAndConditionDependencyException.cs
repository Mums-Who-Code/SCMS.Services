// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class TermsAndConditionDependencyException : Xeption
    {
        public TermsAndConditionDependencyException(Xeption innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException)
        { }
    }
}
