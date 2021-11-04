// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SMCS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class NullGuardianException : Xeption
    {
        public NullGuardianException()
            : base(message: "Guardian is null.")
        { }
    }
}
