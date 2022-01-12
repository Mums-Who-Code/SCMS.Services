// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class FailedGuardianRequestProcessingException : Xeption
    {
        public FailedGuardianRequestProcessingException(Exception innerException)
            : base(message: "Failed guardian request, contact support.", innerException)
        { }
    }
}
