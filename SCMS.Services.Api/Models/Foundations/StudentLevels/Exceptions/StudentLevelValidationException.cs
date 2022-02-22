// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions
{
    public class StudentLevelValidationException : Xeption
    {
        public StudentLevelValidationException(Xeption innerException)
            : base(message: "Student level validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
