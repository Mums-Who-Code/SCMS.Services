// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class StudentGuardianRequestOrchestrationServiceException : Xeption
    {
        public StudentGuardianRequestOrchestrationServiceException(Xeption innerException)
            : base(message: "Student guardian request service error occurred, contact support.",
                  innerException)
        { }
    }
}
