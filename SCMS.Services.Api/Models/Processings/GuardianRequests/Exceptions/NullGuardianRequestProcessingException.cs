// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class NullGuardianRequestProcessingException : Xeption
    {
        public NullGuardianRequestProcessingException()
            : base(message: "Guardian request is null.")
        { }
    }
}
