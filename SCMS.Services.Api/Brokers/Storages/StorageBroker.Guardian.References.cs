// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Guardians;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetGuardianReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guardian>()
                .HasOne(guardian => guardian.CreatedByUser)
                .WithMany(users => users.CreatedGuardians)
                .HasForeignKey(guardian => guardian.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Guardian>()
                .HasOne(guardian => guardian.UpdatedByUser)
                .WithMany(users => users.UpdatedGuardians)
                .HasForeignKey(guardian => guardian.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
