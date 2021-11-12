// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------


using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Student> Students { get; set; }

        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Student> studentEntityEntry =
                await broker.Students.AddAsync(entity: student);

            await broker.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }
      
        public IQueryable<Student> SelectAllStudents() => this.Students.AsQueryable();
      
        public async ValueTask<Student> SelectStudentByIdAsync(Guid studentId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Students.FindAsync(studentId);
        }
    }
}
