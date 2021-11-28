// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class InvalidPhoneReferenceException : Xeption
    {
        public InvalidPhoneReferenceException(Exception innerException)
            : base(message: "Invalid phone reference error occurred, fix the error and try again.",
                  innerException)
        { }
    }
}
