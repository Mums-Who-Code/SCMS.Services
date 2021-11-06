// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class AlreadyExistsStudentSchoolException : Xeption
    {
        public AlreadyExistsStudentSchoolException(Exception innerException)
            : base(message: "Student school with id already exists.", innerException)
        { }
    }
}
