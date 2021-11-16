// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class FailedStudentGuardianServiceException : Xeption
    {
        public FailedStudentGuardianServiceException(Exception innerException)
            : base(message: "Failed student guardian service error occurred.", innerException)
        { }
    }
}
