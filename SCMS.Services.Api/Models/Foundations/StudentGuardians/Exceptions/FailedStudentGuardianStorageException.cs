// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class FailedStudentGuardianStorageException : Xeption
    {
        public FailedStudentGuardianStorageException(Exception innerException)
            : base(message: "Failed student guardian storage error occurred.", innerException)
        { }
    }
}
