// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Emails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetEmailReferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .HasOne(email => email.Guardian)
                .WithOne(guardian => guardian.RegisteredEmail)
                .HasForeignKey<Email>(email => email.GuardianId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(email => email.CreatedByUser)
                .WithMany(users => users.CreatedEmails)
                .HasForeignKey(email => email.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Email>()
                .HasOne(email => email.UpdatedByUser)
                .WithMany(users => users.UpdatedEmails)
                .HasForeignKey(email => email.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
