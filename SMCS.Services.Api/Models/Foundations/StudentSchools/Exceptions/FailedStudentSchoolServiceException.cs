// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class FailedStudentSchoolServiceException : Xeption
    {
        public FailedStudentSchoolServiceException(Exception innerException)
            : base(message: "Failed student school service error occurred, contact support.",
                  innerException)
        { }
    }
}
