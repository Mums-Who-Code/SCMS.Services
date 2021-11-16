// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class InvalidStudentGuardianReferenceException : Xeption
    {
        public InvalidStudentGuardianReferenceException(Exception innerException)
            : base(message: "Invalid student guardian reference error occurred.", innerException)
        { }
    }
}
