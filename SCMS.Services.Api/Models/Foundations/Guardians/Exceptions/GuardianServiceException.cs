// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class GuardianServiceException : Xeption
    {
        public GuardianServiceException(Xeption innerException)
            : base(message: "Guardian service error occurred, contact support.",
                  innerException)
        { }
    }
}
