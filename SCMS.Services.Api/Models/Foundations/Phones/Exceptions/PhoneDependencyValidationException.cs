// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class PhoneDependencyValidationException : Xeption
    {
        public PhoneDependencyValidationException(Xeption innerException)
            : base(message: "Phone dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
