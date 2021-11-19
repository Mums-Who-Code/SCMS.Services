// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;

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
