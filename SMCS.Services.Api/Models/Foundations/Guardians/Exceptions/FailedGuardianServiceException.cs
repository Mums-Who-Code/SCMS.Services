// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class FailedGuardianServiceException : Xeption
    {
        public FailedGuardianServiceException(Exception innerException)
            : base(message: "Failed guardian service error occurred, contact support.",
                  innerException)
        { }
    }
}
