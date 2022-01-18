// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class StudentGuardianRequestOrchestrationValidationException : Xeption
    {
        public StudentGuardianRequestOrchestrationValidationException(Xeption innerException)
            : base(message: "Student guardian request validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
