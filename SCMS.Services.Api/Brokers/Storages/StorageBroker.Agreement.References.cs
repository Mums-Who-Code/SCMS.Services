// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Agreements;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public static void SetAgreementReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agreement>()
                .HasOne(agreement => agreement.CreatedByUser)
                .WithMany(user => user.CreatedAgreements)
                .HasForeignKey(agreement => agreement.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Agreement>()
                .HasOne(agreement => agreement.UpdatedByUser)
                .WithMany(user => user.UpdatedAgreements)
                .HasForeignKey(agreement => agreement.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
