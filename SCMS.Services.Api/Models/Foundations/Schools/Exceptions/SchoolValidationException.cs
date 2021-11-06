// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class SchoolValidationException : Xeption
    {
        public SchoolValidationException(Exception innerException)
            : base(message: "School validation error occurred, fix the error and try again.", innerException)
        { }
    }
}
