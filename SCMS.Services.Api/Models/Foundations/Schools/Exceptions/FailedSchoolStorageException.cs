// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class FailedSchoolStorageException : Xeption
    {
        public FailedSchoolStorageException(Exception innerException)
            : base(message: "Failed school storage error occurred, contact support.", innerException)
        { }
    }
}
