// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class NullStudentSchoolException : Xeption
    {
        public NullStudentSchoolException()
            : base(message: "Student school is null.")
        { }
    }
}
