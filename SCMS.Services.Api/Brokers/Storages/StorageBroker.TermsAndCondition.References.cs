// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SetTermsAndConditionReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TermsAndCondition>()
                .HasOne(termsAndCondition => termsAndCondition.CreatedByUser)
                .WithMany(user => user.CreatedTermsAndCondition)
                .HasForeignKey(termsAndCondition => termsAndCondition.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TermsAndCondition>()
                .HasOne(termsAndCondition => termsAndCondition.UpdatedByUser)
                .WithMany(user => user.UpdatedTermsAndCondition)
                .HasForeignKey(termsAndCondition => termsAndCondition.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
