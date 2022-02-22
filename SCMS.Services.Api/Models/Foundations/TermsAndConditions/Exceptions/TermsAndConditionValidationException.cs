// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class TermsAndConditionValidationException : Xeption
    {
        public TermsAndConditionValidationException(Exception innerException)
            : base(message: "Terms And Condition validation error occured, fix the error and try again.",
                    innerException)
        { }
    }
}
