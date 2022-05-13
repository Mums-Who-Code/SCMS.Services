// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Agreements.Exceptions
{
    public class InvalidAgreementException : Xeption
    {
        public InvalidAgreementException()
            : base(message: "Invalid agreement, please fix the errors and try again.")
        { }
    }
}
