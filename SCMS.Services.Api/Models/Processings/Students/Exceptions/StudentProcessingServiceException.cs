// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class StudentProcessingServiceException : Xeption
    {
        public StudentProcessingServiceException(Xeption innerException)
            : base(message: "Student service error occurred, contact support.", innerException)
        { }
    }
}
