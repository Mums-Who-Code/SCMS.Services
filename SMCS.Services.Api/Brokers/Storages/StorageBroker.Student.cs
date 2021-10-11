// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SMCS.Services.Api.Models.Foundations.Students;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Student> Students { get; set; }

        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            EntityEntry<Student> studentEntityEntry =
                await this.Students.AddAsync(entity: student);

            await this.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }
    }
}
