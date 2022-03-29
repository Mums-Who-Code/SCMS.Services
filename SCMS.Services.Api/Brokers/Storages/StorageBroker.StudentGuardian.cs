// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public IQueryable<StudentGuardian> SelectAllStudentGuardians() => this.StudentGuardians;

        public async ValueTask<StudentGuardian> SelectStudentGuardianByIdAsync(Guid studentGuardianId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.StudentGuardians.FindAsync(studentGuardianId);
        }

        public async ValueTask<StudentGuardian> UpdateStudentGuardianAsync(StudentGuardian studentGuardian)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<StudentGuardian> entityEntry = broker.StudentGuardians.Update(entity: studentGuardian);
            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async ValueTask<StudentGuardian> DeleteStudentGuardianAsync(StudentGuardian studentGuardian)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<StudentGuardian> EntityEntry = broker.StudentGuardians.Remove(entity: studentGuardian);
            await broker.SaveChangesAsync();

            return EntityEntry.Entity;
        }
    }
}
