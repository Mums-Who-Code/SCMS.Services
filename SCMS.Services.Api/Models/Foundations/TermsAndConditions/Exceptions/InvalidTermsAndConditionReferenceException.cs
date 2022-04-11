// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class InvalidTermsAndConditionReferenceException : Xeption
    {
        public InvalidTermsAndConditionReferenceException(Exception innerException)
            : base(message: "Invalid terms and condition reference error occurred.", innerException)
        { }
    }
}
