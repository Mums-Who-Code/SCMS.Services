// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class InvalidStudentGuardianRequestOrchestrationException : Xeption
    {
        public InvalidStudentGuardianRequestOrchestrationException()
            : base(message: "Invalid student guardian request, fix the errors and try again.")
        { }
    }
}
