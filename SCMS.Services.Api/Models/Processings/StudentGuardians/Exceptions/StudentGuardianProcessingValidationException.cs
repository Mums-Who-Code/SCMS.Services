// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class StudentGuardianProcessingValidationException : Xeption
    {
        public StudentGuardianProcessingValidationException(Xeption innerException)
            : base(message: "Student guardian processing validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
