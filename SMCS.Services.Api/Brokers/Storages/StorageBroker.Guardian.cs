// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SMCS.Services.Api.Models.Foundations.Guardians;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Guardian> Guardians { get; set; }

        public async ValueTask<Guardian> InsertGuardianAsync(Guardian guardian)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Guardian> entityEntry =
                await broker.Guardians.AddAsync(guardian);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
