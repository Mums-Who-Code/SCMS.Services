// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Branches;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetBranchReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .HasKey(branch =>
                    new { branch.Name });

            modelBuilder.Entity<Branch>()
                .HasOne(branch => branch.CreatedByUser)
                .WithMany(user => user.CreatedBranches)
                .HasForeignKey(branch => branch.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Branch>()
                .HasOne(branch => branch.UpdatedByUser)
                .WithMany(user => user.UpdatedBranches)
                .HasForeignKey(branch => branch.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
