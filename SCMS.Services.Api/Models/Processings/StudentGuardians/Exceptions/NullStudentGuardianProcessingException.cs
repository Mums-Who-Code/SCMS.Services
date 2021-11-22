// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class NullStudentGuardianProcessingException : Xeption
    {
        public NullStudentGuardianProcessingException()
            : base(message: "Student guardian is null.")
        { }
    }
}
