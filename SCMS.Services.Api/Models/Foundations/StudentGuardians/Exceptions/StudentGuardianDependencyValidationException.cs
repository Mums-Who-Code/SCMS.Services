// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class StudentGuardianDependencyValidationException : Xeption
    {
        public StudentGuardianDependencyValidationException(Xeption innerException)
            : base(message: "Student guardian dependency validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
