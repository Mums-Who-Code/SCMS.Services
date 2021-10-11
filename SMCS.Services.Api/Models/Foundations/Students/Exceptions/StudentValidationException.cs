// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class StudentValidationException : Xeption
    {
        public StudentValidationException(Exception innerException)
            : base(message: "Invalid input, contact support.", innerException)
        { }
    }
}
