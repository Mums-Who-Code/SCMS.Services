// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class SchoolDependencyValidationException : Xeption
    {
        public SchoolDependencyValidationException(Xeption innerException)
            : base(message: "School dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
