// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SMCS.Services.Api.Models.Foundations.Guardians;
using SMCS.Services.Api.Models.Foundations.Guardians.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.Guardians
{
    public partial class GuardianService
    {

        private void ValidateGuardianOnAdd(Guardian student)
        {
            if (student == null)
            {
                throw new NullGuardianException();
            }
        }
    }
}
