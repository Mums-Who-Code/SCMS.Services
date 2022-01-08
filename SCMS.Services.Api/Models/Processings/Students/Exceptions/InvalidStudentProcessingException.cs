// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class InvalidStudentProcessingException : Xeption
    {
        public InvalidStudentProcessingException()
            : base(message: "Invalid student, fix the error and try again.")
        { }
    }
}
