// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions
{
    public class InvalidStudentLevelException : Xeption
    {
        public InvalidStudentLevelException()
            : base(message: "Invalid studentlevel.")
        { }
    }
}
