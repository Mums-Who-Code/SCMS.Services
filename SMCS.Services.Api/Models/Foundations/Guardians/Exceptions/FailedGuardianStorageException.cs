// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class FailedGuardianStorageException : Xeption
    {
        public FailedGuardianStorageException(Exception innerException)
            : base(message: "Failed guardian storage error occurred, contact support.", innerException)
        { }
    }
}
