// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements.Exceptions
{
    public class PersonValidationException : Xeption
    {
        public PersonValidationException(Xeption innerException)
            : base(message: "Person validation error occurd,fix the error and try again.",
                  innerException)
        { }
    }
}