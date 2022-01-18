// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class FailedStudentGuardianRequestOrchestrationException : Xeption
    {
        public FailedStudentGuardianRequestOrchestrationException(Exception innerException)
            : base(message: "Failed student guardian request error occurred, contact support.",
                  innerException)
        { }
    }
}
