// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolService
    {
        private static void ValidateStudentSchool(StudentSchool studentSchool)
        {
            if (studentSchool is null)
            {
                throw new NullStudentSchoolException();
            }
        }
    }
}
