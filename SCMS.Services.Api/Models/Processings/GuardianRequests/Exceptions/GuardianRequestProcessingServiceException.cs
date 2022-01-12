// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class GuardianRequestProcessingServiceException : Xeption
    {
        public GuardianRequestProcessingServiceException(Xeption innerException)
            : base(message: "Guardian request service error occurred, contact support.",
                  innerException)
        { }
    }
}
