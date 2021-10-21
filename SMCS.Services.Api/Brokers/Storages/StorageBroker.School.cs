// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<School> Schools { get; set; }

        public async ValueTask<School> InsertSchoolAsync(School school)
        {
            EntityEntry<School> entityEntry =
                await this.Schools.AddAsync(entity: school);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
