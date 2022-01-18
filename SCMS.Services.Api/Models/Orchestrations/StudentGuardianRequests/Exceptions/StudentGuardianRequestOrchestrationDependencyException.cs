// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class StudentGuardianRequestOrchestrationDependencyException : Xeption
    {
        public StudentGuardianRequestOrchestrationDependencyException(Xeption innerException)
            : base(message: "Student guardian request dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
