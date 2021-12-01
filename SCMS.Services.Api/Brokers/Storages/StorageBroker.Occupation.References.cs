// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Occupations;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetOccupationReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupation>()
                .HasOne(occupation => occupation.CreatedByUser)
                .WithMany(users => users.CreatedOccupations)
                .HasForeignKey(occupation => occupation.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Occupation>()
                .HasOne(occupation => occupation.UpdatedByUser)
                .WithMany(users => users.UpdatedOccupations)
                .HasForeignKey(occupation => occupation.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
