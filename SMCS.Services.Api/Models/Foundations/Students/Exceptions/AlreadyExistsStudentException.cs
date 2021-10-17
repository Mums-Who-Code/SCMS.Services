// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SMCS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class AlreadyExistsStudentException : Exception
    {
        public AlreadyExistsStudentException(Exception innerException)
            : base(message: "Student with the same id already exists.", innerException)
        { }
    }
}
