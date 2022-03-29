// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Guardians;
using System;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Foundations.Guardians
{
    public interface IGuardianService
    {
        ValueTask<Guardian> AddGuardianAsync(Guardian guardian);
        ValueTask<Guardian> RetrieveGuardianByIdAsync(Guid guardianId);
    }
}
