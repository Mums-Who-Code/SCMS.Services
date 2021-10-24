// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class FailedSchoolServiceException : Xeption
    {
        public FailedSchoolServiceException(Exception innerException)
            : base(message: "Failed school service error occurred, contact support.", innerException)
        { }
    }
}
