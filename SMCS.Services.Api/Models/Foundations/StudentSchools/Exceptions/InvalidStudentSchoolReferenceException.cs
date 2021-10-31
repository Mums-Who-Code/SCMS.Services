// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions
{
    public class InvalidStudentSchoolReferenceException : Xeption
    {
        public InvalidStudentSchoolReferenceException(Exception innerException)
            : base(message: "Invalid school student reference error occurred, fix the erros and try again.",
                  innerException)
        { }
    }
}
