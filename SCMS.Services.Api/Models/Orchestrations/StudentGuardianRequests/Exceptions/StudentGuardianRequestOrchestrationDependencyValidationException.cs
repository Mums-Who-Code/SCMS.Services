// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class StudentGuardianRequestOrchestrationDependencyValidationException : Xeption
    {
        public StudentGuardianRequestOrchestrationDependencyValidationException(Xeption innerException)
            : base(message: "Student guardian request dependency validation error occurred," +
                  " fix the errors and try again.", innerException)
        { }
    }
}
