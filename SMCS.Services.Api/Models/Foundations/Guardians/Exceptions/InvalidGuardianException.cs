// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class InvalidGuardianException : Xeption
    {
        public InvalidGuardianException()
            : base(message: "Invalid guardian input, fix the error and try again.")
        { }
    }
}
