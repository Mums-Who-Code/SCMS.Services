// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class InvalidStudentGuardianProcessingException : Xeption
    {
        public InvalidStudentGuardianProcessingException()
            : base(message: "Invalid student guardian.")
        { }
    }
}
