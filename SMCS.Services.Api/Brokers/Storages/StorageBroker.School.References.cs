// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void AddSchoolReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .HasOne(school => school.CreatedByUser)
                .WithMany(user => user.CreatedSchools)
                .HasForeignKey(school => school.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<School>()
                .HasOne(school => school.UpdatedByUser)
                .WithMany(user => user.UpdatedSchools)
                .HasForeignKey(school => school.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
