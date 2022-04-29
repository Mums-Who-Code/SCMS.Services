// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Agreements.Exceptions
{
    public class NullAgreementException : Xeption
    {
        public NullAgreementException()
            : base(message: "Agreement is null.")
        { }
    }
}
