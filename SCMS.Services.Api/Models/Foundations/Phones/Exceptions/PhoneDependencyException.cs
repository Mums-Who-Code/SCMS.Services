// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class PhoneDependencyException : Xeption
    {
        public PhoneDependencyException(Xeption innerException)
            : base(message: "Phone dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
