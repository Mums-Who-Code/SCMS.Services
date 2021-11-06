// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class InvalidStudentReferenceException : Xeption
    {
        public InvalidStudentReferenceException(Exception innerException)
            : base(message:"Invalid student reference error occurred, contact support")
        { }
    }
}
