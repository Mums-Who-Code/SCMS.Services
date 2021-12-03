// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.AdditionalDetails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<AdditionalDetail> AdditionalDetails { get; set; }

        public async ValueTask<AdditionalDetail> InsertAdditionalDetailAsync(AdditionalDetail additionalDetail)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<AdditionalDetail> entityEntry =
                await broker.AdditionalDetails.AddAsync(additionalDetail);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<AdditionalDetail> SelectAllAdditionalDetails() => this.AdditionalDetails;
    }
}
