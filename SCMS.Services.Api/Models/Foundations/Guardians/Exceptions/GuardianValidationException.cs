// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class GuardianValidationException : Xeption
    {
        public GuardianValidationException(Xeption innerException)
            : base(message: "Guardian validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
