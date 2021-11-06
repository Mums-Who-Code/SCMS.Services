// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class StudentSchoolDependencyException : Xeption
    {
        public StudentSchoolDependencyException(Xeption innerException)
            : base(message: "Student school dependency error occurred, contact support.", innerException)
        { }
    }
}
