// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class GuardianDependencyValidationException : Xeption
    {
        public GuardianDependencyValidationException(Xeption innerException)
            : base(message: "Guardian dependency validation error occurred, fix the error and try agian.",
                 innerException)
        { }
    }
}
