// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SMCS.Services.Api.Models.Foundations.StudentSchools;

namespace SMCS.Services.Api.Brokers.Storages
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
    }
}
