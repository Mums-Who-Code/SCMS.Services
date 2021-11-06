// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class NullSchoolException : Xeption
    {
        public NullSchoolException()
            : base(message: "School is null.")
        { }
    }
}
