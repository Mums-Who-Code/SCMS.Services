// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class StudentSchoolValidationException : Xeption
    {
        public StudentSchoolValidationException(Xeption innerException)
            : base(message: "Student school validation error occurred.", innerException)
        { }
    }
}
