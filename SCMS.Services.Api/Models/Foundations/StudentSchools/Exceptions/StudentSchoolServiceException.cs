// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class StudentSchoolServiceException : Xeption
    {
        public StudentSchoolServiceException(Xeption innerException)
            : base(message: "Student school service error occurred, contact support.",
                  innerException)
        { }
    }
}
