// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.Schools.Exceptions
{
    public class AlreadyExistsSchoolException : Xeption
    {
        public AlreadyExistsSchoolException(Exception innerException)
            : base(message: "School with the same id already exists.", innerException)
        { }
    }
}
