// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class PhoneValidationException : Xeption
    {
        public PhoneValidationException(Xeption innerException)
            : base(message: "Phone validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
