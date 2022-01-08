// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class FailedGuardianDependencyException : Xeption
    {
        public FailedGuardianDependencyException(Exception innerException)
            : base(message: "Failed guardian dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
