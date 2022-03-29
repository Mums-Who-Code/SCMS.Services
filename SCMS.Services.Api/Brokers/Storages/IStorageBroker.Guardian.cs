// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Guardians;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guardian> InsertGuardianAsync(Guardian guardian);
        IQueryable<Guardian> SelectAllGuardians();
        ValueTask<Guardian> SelectGuardianByIdAsync(Guid guardianId);
        ValueTask<Guardian> UpdateGuardianAsync(Guardian guardian);
        ValueTask<Guardian> DeleteGuardianAsync(Guardian guardian);
    }
}
