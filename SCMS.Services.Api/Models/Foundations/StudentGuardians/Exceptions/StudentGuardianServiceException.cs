// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class StudentGuardianServiceException : Xeption
    {
        public StudentGuardianServiceException(Xeption innerException)
            : base(message: "Student guardian service error occurred, contact support.", innerException)
        { }
    }
}
