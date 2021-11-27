// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class FailedPhoneStorageException : Xeption
    {
        public FailedPhoneStorageException(Exception innerException)
            : base(message: "Failed phone storage, contact support.",
                  innerException)
        { }
    }
}
