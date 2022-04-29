// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Agreements.Exceptions
{
    public class AgreementValidationException : Xeption
    {
        public AgreementValidationException(Xeption innerException)
            : base(message: "Agreement validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
