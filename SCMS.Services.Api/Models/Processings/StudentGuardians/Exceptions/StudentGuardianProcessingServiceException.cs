// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class StudentGuardianProcessingServiceException : Xeption
    {
        public StudentGuardianProcessingServiceException(Xeption innerException)
            : base(message: "Student guardian processing service error occurred, contact support.",
                  innerException)
        { }
    }
}
