// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Guardians;

namespace SCMS.Services.Api.Brokers.Storages
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

        public IQueryable<Guardian> SelectAllGuardians() => this.Guardians;

        public async ValueTask<Guardian> SelectGuardianByIdAsync(Guid guardianId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Guardians.FindAsync(guardianId);
        }

        public async ValueTask<Guardian> UpdateGuardianAsync(Guardian guardian)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Guardian> guardianEntityEntry = broker.Guardians.Update(entity: guardian);
            await broker.SaveChangesAsync();

            return guardianEntityEntry.Entity;
        }
    }
}
