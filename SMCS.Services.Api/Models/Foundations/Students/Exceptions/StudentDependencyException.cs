// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class StudentDependencyException : Xeption
    {
        public StudentDependencyException(Xeption innerException)
            : base(message: "Service dependency error occurred, contact support.", innerException)
        { }
    }
}
