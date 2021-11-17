// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class AlreadyPrimaryStudentGuardianExistsException : Xeption
    {
        public AlreadyPrimaryStudentGuardianExistsException()
            : base(message: "Primary contact already exists.")
        { }
    }
}
