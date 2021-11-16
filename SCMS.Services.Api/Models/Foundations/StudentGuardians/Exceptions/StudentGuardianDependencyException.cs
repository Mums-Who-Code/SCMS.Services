// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class StudentGuardianDependencyException : Xeption
    {
        public StudentGuardianDependencyException(Xeption innerException)
            : base(message: "Student guardian dependency error occurred, contact support.", innerException)
        { }
    }
}
