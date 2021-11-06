// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class StudentDependencyException : Xeption
    {
        public StudentDependencyException(Xeption innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException)
        { }
    }
}
