// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class StudentSchoolDependencyValidationException : Xeption
    {
        public StudentSchoolDependencyValidationException(Xeption innerException)
            : base(message: "Student school dependency validation error occurred, fix the errors and try again",
                  innerException)
        { }
    }
}
