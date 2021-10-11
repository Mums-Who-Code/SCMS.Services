// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Students;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void AddStudentReferences(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Student>()
                .HasOne(student => student.CreatedByUser)
                .WithMany(user => user.CreatedStudents)
                .HasForeignKey(student => student.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelbuilder.Entity<Student>()
                .HasOne(student => student.UpdatedByUser)
                .WithMany(user => user.UpdatedStudents)
                .HasForeignKey(student => student.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
