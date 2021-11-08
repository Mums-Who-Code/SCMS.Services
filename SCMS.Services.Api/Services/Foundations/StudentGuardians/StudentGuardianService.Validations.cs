// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianService : IStudentGuardianService
    {
        private static void ValidateStudentGuardian(StudentGuardian studentGuardian)
        {
            if (studentGuardian is null)
            {
                throw new NullStudentGuardianException();
            }
        }
    }
}
