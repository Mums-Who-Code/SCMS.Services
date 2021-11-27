// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class FailedPhoneServiceException : Xeption
    {
        public FailedPhoneServiceException(Exception innerException)
            : base(message: "Failed phone service error occurred, contact support.",
                  innerException)
        { }
    }
}
