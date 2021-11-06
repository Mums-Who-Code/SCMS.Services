// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class FailedStudentSchoolStorageException : Xeption
    {
        public FailedStudentSchoolStorageException(Exception innerException)
            : base(message: "Failed student school storage error occurred, contact support.", innerException)
        { }
    }
}
