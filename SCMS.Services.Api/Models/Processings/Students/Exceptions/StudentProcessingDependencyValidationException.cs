// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class StudentProcessingDependencyValidationException : Xeption
    {
        public StudentProcessingDependencyValidationException(Xeption innerException)
            : base(message: "Student dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
