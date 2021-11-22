// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetPhoneReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>()
                .HasOne(phone => phone.Guardian)
                .WithOne(guardian => guardian.RegisteredPhone)
                .HasForeignKey<Phone>(phone => phone.GuardianId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Phone>()
                .HasOne(phone => phone.CreatedByUser)
                .WithMany(users => users.CreatedPhones)
                .HasForeignKey(phone => phone.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Phone>()
                .HasOne(phone => phone.UpdatedByUser)
                .WithMany(users => users.UpdatedPhones)
                .HasForeignKey(phone => phone.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
