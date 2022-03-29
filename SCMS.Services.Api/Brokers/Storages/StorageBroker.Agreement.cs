// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Agreements;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Agreement> Agreements { get; set; }

        public async ValueTask<Agreement> InsertAgreementAsync(Agreement agreement)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Agreement> agreementEntityEntry =
                await broker.Agreements.AddAsync(agreement);

            await broker.SaveChangesAsync();

            return agreementEntityEntry.Entity;
        }
    }
}
