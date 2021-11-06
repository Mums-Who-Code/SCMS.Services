// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Guardians.Exceptions
{
    public class GuardianDependencyException : Xeption
    {
        public GuardianDependencyException(Xeption innerException)
            : base(message: "Guardain dependency error occurred, contact support.", innerException)
        { }
    }
}
