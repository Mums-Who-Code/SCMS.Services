// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class StudentProcessingDependencyException : Xeption
    {
        public StudentProcessingDependencyException(Xeption innerException)
            : base(message: "Student dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
