// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class AlreadyExistsTermsAndConditionException : Xeption
    {
        public AlreadyExistsTermsAndConditionException(Exception innerException)
            : base(message: "Terms and condition already exists.",
                  innerException)
        { }
    }
}
