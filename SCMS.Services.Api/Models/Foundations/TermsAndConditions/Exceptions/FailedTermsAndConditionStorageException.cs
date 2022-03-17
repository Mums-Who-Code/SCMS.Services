// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class FailedTermsAndConditionStorageException : Xeption
    {
        public FailedTermsAndConditionStorageException(Exception innerException)
            : base(message: "Failed terms and condition storage error occurred, contact support.", innerException)
        { }
    }
}
