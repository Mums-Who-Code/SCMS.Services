// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class InvalidGuardianReferenceException : Xeption
    {
        public InvalidGuardianReferenceException(Exception innerException)
            : base(message: "Invalid guardian rerference error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
