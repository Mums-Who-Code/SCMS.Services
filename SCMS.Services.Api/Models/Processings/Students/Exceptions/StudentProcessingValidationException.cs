// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class StudentProcessingValidationException : Xeption
    {
        public StudentProcessingValidationException(Xeption innerException)
            : base(message: "Student validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
