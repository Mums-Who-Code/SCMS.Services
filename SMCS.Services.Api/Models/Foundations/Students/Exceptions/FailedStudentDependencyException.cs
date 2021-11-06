// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class FailedStudentDependencyException : Xeption
    {
        public FailedStudentDependencyException(Exception innerException)
            : base(message: "Failed student dependency error occurred, contact support.", innerException)
        { }
    }
}
