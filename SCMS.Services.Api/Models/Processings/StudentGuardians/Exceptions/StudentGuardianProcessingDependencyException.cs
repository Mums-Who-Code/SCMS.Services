// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class StudentGuardianProcessingDependencyException : Xeption
    {
        public StudentGuardianProcessingDependencyException(Xeption innerException)
            : base(message: "Student guardian processing dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
