// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetStudentGuardianReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGuardian>()
                .HasKey(studentGuardian =>
                    new { studentGuardian.StudentId, studentGuardian.GuardianId });

            modelBuilder.Entity<StudentGuardian>()
                .HasOne(studentGuardian => studentGuardian.CreatedByUser)
                .WithMany(user => user.CreatedStudentGuardians)
                .HasForeignKey(studentGuardian => studentGuardian.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentGuardian>()
                .HasOne(studentGuardian => studentGuardian.UpdatedByUser)
                .WithMany(user => user.UpdatedStudentGuardians)
                .HasForeignKey(studentGuardian => studentGuardian.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
