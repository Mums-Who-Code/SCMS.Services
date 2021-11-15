// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class StudentGuardianValidationException : Xeption
    {
        public StudentGuardianValidationException(Xeption innerException)
            : base(message: "Student guardian validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
