// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Students.Exceptions
{
    public class NullStudentException : Xeption
    {
        public NullStudentException()
            : base(message: "The student is null.")
        { }
    }
}
