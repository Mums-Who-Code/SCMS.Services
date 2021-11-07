// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<StudentGuardian> StudentGuardians { get; set; }

        public async ValueTask<StudentGuardian> InsertStudentGuardianAsync(StudentGuardian studentGuardian)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<StudentGuardian> entityEntry =
                await broker.StudentGuardians.AddAsync(studentGuardian);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
