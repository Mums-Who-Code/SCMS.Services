// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class FailedStudentGuardianProcessingServiceException : Xeption
    {
        public FailedStudentGuardianProcessingServiceException(Exception innerException)
            : base(message: "Failed student guardian processing service exception.", innerException)
        { }
    }
}
