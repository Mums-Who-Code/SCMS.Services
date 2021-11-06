// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class AlreadyExistsStudentException : Xeption
    {
        public AlreadyExistsStudentException(Exception innerException)
            : base(message: "Student with the same id already exists.", innerException)
        { }
    }
}
