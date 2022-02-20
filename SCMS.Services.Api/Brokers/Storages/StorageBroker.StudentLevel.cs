// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<StudentLevel> StudentLevels { get; set; }

        public async ValueTask<StudentLevel> InsertStudentLevelAsync(StudentLevel studentLevel)
        {
            EntityEntry<StudentLevel> entityEntry =
                await this.StudentLevels.AddAsync(entity: studentLevel);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
