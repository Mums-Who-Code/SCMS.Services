// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.AdditionalDetails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetAdditionalDetailReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalDetail>()
                .HasOne(additionalDetailUser => additionalDetailUser.CreatedByUser)
                .WithMany(user => user.CreatedAdditionalDetails)
                .HasForeignKey(additionalDetailUser => additionalDetailUser.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AdditionalDetail>()
                .HasOne(additionalDetailUser => additionalDetailUser.UpdatedByUser)
                .WithMany(user => user.UpdatedAdditionalDetails)
                .HasForeignKey(additionalDetailUser => additionalDetailUser.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
