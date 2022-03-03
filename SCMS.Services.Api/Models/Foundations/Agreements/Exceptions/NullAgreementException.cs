// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements.Exceptions
{
    public class NullAgreementException : Xeption
    {
        public NullAgreementException()
            : base(message: "Agreement is null")
        { }
    }
}