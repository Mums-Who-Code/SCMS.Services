// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.StudentSchools;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<StudentSchool> StudentSchools { get; set; }

        public async ValueTask<StudentSchool> InsertStudentSchoolAsync(StudentSchool studentSchool)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<StudentSchool> entityEntry =
                await broker.StudentSchools.AddAsync(studentSchool);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<StudentSchool> SelectAllStudentSchools() => this.StudentSchools;

        public async ValueTask<StudentSchool> SelectStudentSchoolByIdAsync(Guid studentSchoolId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.StudentSchools.FindAsync(studentSchoolId);
        }

        public async ValueTask<StudentSchool> UpdateStudentSchoolAsync(StudentSchool studentSchool)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<StudentSchool> studentSchoolEntityEntry =
                broker.StudentSchools.Update(entity: studentSchool);

            await broker.SaveChangesAsync();

            return studentSchoolEntityEntry.Entity;
        }
      
        public async ValueTask<StudentSchool> DeleteStudentSchoolAsync(StudentSchool studentSchool)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<StudentSchool> studentSchoolEntityEntry =
                broker.StudentSchools.Remove(entity: studentSchool);
  
            await broker.SaveChangesAsync();

            return studentSchoolEntityEntry.Entity;
        }
    }
}
