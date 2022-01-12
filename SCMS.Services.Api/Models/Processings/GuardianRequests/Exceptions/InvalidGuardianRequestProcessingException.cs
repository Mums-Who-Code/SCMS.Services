// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions
{
    public class InvalidGuardianRequestProcessingException : Xeption
    {
        public InvalidGuardianRequestProcessingException()
            : base(message: "Invalid guardian request, fix the errors and try again.")
        { }
    }
}
