// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class PhoneServiceException : Xeption
    {
        public PhoneServiceException(Xeption innerException)
            : base(message: "Phone service error occurred, contact support.",
                  innerException)
        { }
    }
}
