// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class InvalidStudentSchoolException : Xeption
    {
        public InvalidStudentSchoolException()
            : base(message: "Invalid student school, fix the errors and try again.")
        { }
    }
}
