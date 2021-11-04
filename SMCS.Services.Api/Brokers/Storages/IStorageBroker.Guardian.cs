// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.Guardians;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<Guardian> InsertGuardianAsync(Guardian guardian);
    }
}
