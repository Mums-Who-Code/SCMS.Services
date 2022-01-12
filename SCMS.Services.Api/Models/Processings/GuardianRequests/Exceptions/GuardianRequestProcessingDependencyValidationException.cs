// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class GuardianRequestProcessingDependencyValidationException : Xeption
    {
        public GuardianRequestProcessingDependencyValidationException(Xeption innerException)
            : base(message: "Guardian request dependency validation error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
