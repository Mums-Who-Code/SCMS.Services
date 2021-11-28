// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Phones.Exceptions
{
    public class AlreadyExistsPhoneException : Xeption
    {
        public AlreadyExistsPhoneException(Exception innerException)
            : base(message: "Phone with same id already exists, try again.",
                  innerException)
        { }
    }
}
