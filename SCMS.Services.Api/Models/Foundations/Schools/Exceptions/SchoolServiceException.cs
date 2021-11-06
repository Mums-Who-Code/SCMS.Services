// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class SchoolServiceException : Xeption
    {
        public SchoolServiceException(Xeption innerException)
            : base(message: "School service error occurred, contact support.", innerException)
        { }
    }
}
