// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Occupations;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Occupation> Occupations { get; set; }

        public async ValueTask<Occupation> InsertOccupationAsync(Occupation occupation)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Occupation> entityEntry =
                await broker.Occupations.AddAsync(occupation);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
