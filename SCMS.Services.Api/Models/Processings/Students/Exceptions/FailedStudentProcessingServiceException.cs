// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class FailedStudentProcessingServiceException : Xeption
    {
        public FailedStudentProcessingServiceException(Exception innerException)
            : base(message: "Failed student service, contact support.", innerException)
        { }
    }
}
