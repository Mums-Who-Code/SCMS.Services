// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions
{
    public class NullStudentGuardianRequestOrchestrationException : Xeption
    {
        public NullStudentGuardianRequestOrchestrationException()
            : base(message: "Student guardian request is null.")
        { }
    }
}
