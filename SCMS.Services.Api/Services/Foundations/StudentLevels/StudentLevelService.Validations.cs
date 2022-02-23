// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentLevels;
using SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.StudentLevels
{
    public partial class StudentLevelService
    {
        private static void ValidateStudentLevel(StudentLevel studentLevel)
        {
            if (studentLevel == null)
            {
                throw new NullStudentLevelException();
            }
        }
    }
}
