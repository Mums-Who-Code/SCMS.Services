// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetStudentReferences(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Student>()
                .HasOne(student => student.EnrolledSchool)
                .WithMany(school => school.EnrolledStudents)
                .HasForeignKey(student => student.SchoolId)
                .OnDelete(DeleteBehavior.NoAction);

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
