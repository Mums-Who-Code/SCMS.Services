// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class InvalidStudentGuardianException : Xeption
    {
        public InvalidStudentGuardianException()
            : base(message: "Invalid student guardian.")
        { }
    }
}
