// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class StudentGuardianProcessingDependencyValidationException : Xeption
    {
        public StudentGuardianProcessingDependencyValidationException(Xeption innerException)
            : base(message: "Student guardian processing dependency validation error occurred," +
                  "fix the errors and try again.",
                    innerException)
        { }
    }
}
