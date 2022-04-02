// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class TermsAndConditionServiceException : Xeption
    {
        public TermsAndConditionServiceException(Xeption innerException)
           : base(message: "Terms and condition service error occured, contact support.", innerException)
        { }
    }
}
