// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class StudentGuardianValidationProcessingException : Xeption
    {
        public StudentGuardianValidationProcessingException(Xeption innerException)
            : base(message: "Student guardian processing validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
