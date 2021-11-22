// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions
{
    public class AlreadyExistsPrimaryStudentGuardianProcessingException : Xeption
    {
        public AlreadyExistsPrimaryStudentGuardianProcessingException(Guid guardianId)
            : base(message: $"Primary student guardian with id : {guardianId} already exists.")
        { }
    }
}
