// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class FailedTermsAndConditionServiceException : Xeption
    {
        public FailedTermsAndConditionServiceException(Exception innerException)
            : base(message: "Failed terms and condition service error occurred.", innerException)
        { }
    }
}
