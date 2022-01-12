// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class GuardianRequestProcessingDependencyException : Xeption
    {
        public GuardianRequestProcessingDependencyException(Xeption innerException)
            : base(message: "Guardian request dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
