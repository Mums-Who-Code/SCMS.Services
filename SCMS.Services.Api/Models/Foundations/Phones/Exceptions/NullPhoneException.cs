// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class NullPhoneException : Xeption
    {
        public NullPhoneException()
            : base(message: "Phone is null.")
        { }
    }
}
