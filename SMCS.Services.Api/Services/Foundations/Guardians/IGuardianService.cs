// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.Guardians;

namespace SMCS.Services.Api.Services.Foundations.Guardians
{
    public interface IGuardianService
    {
        ValueTask<Guardian> AddGuardianAsync(Guardian guardian);
    }
}
