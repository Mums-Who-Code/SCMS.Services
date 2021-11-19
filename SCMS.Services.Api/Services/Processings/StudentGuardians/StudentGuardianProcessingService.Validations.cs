// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService
    {
        private static void ValidateStudentGuardian(StudentGuardian studentGuardian)
        {
            if (studentGuardian == null)
            {
                throw new NullStudentGuardianProcessingException();
            }
        }
    }
}
