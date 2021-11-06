// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class AlreadyExistsGuardianException : Xeption
    {
        public AlreadyExistsGuardianException(Exception innerException)
            : base(message: "Guardian with same id already exists.", innerException)
        { }
    }
}
