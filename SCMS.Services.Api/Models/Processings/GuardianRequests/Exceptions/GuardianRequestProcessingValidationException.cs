// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class GuardianRequestProcessingValidationException : Xeption
    {
        public GuardianRequestProcessingValidationException(Xeption innerException)
            : base(message: "Guardian request validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
