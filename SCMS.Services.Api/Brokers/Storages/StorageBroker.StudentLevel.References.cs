// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetStudentLevelReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentLevel>()
                .HasKey(studentLevel =>
                    new { studentLevel.Name });

            modelBuilder.Entity<StudentLevel>()
                .HasOne(studentLevel => studentLevel.CreatedByUser)
                .WithMany(user => user.CreatedStudentLevels)
                .HasForeignKey(studentLevel => studentLevel.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentLevel>()
                .HasOne(studentLevel => studentLevel.UpdatedByUser)
                .WithMany(user => user.UpdatedStudentLevels)
                .HasForeignKey(studentLevel => studentLevel.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
