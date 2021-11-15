// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions
{
    public class AlreadyExistsStudentGuardianException : Xeption
    {
        public AlreadyExistsStudentGuardianException(Exception innerException)
            : base(message: "Student guardian with id already exists.", innerException)
        { }
    }
}
