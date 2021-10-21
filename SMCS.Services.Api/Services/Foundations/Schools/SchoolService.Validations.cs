// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Models.Foundations.Schools.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.Schools
{
    public partial class SchoolService
    {
        private static void ValidateSchool(School school)
        {
            if (school is null)
            {
                throw new NullSchoolException();
            }
        }
    }
}
