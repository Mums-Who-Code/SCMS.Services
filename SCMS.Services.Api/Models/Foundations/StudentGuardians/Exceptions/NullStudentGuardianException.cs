// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class NullStudentGuardianException : Xeption
    {
        public NullStudentGuardianException()
            : base(message: "Student guardian is null.")
        { }
    }
}
