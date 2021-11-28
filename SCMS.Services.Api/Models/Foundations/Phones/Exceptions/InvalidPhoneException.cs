// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class InvalidPhoneException : Xeption
    {
        public InvalidPhoneException()
            : base(message: "Invalid phone, fix the errors and try again.")
        { }
    }
}
