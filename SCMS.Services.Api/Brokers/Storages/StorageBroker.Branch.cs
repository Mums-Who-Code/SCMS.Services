// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Branches;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Branch> Branches { get; set; }

        public async ValueTask<Branch> InsertBranchAsync(Branch branch)
        {
            EntityEntry<Branch> entityEntry =
                await this.Branches.AddAsync(entity: branch);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
