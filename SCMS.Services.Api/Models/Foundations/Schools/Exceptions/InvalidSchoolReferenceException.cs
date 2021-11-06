// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class InvalidSchoolReferenceException : Xeption
    {
        public InvalidSchoolReferenceException(Exception innerException)
            : base(message: "Invalid school reference error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
