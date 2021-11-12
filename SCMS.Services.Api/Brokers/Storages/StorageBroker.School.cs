// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<School> Schools { get; set; }

        public async ValueTask<School> InsertSchoolAsync(School school)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<School> entityEntry =
                await broker.Schools.AddAsync(entity: school);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<School> SelectAllSchools() => this.Schools;
    }
}
