// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class RepeatedStudentSchoolException : Xeption
    {
        public RepeatedStudentSchoolException(Exception innerException)
            : base(message: "Repeated student school error occurred, fix the errors and try again.", innerException)
        { }
    }
}
