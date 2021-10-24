// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class SchoolDependencyException : Xeption
    {
        public SchoolDependencyException(Xeption innerException)
            : base(message: "School dependency error occurred, contact support.", innerException)
        { }
    }
}
