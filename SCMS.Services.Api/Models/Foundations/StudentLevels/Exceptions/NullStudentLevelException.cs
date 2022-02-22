// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions
{
    public class NullStudentLevelException : Xeption
    {
        public NullStudentLevelException()
            : base(message: "Student level is null.")
        { }
    }
}
