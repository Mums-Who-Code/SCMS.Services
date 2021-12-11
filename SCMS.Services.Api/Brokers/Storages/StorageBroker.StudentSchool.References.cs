﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentSchools;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetStudentSchoolReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentSchool>()
                .HasKey(studentSchool =>
                    new { studentSchool.StudentId, studentSchool.SchoolId });

            modelBuilder.Entity<StudentSchool>()
                .HasOne(studentSchool => studentSchool.CreatedByUser)
                .WithMany(user => user.CreatedStudentSchools)
                .HasForeignKey(studentSchool => studentSchool.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentSchool>()
                .HasOne(studentSchool => studentSchool.UpdatedByUser)
                .WithMany(user => user.UpdatedStudentSchools)
                .HasForeignKey(studentSchool => studentSchool.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
