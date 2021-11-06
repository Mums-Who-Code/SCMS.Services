// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Guardians;

namespace SCMS.Services.Api.Services.Foundations.Guardians
{
    public interface IGuardianService
    {
        ValueTask<Guardian> AddGuardianAsync(Guardian guardian);
    }
}
