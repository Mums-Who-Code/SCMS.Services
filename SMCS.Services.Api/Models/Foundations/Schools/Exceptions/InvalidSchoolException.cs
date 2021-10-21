// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class InvalidSchoolException : Xeption
    {
        public InvalidSchoolException()
            : base(message: "Invalid school, fix the errors and try again.")
        { }
    }
}
