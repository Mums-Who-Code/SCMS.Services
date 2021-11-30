// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Emails;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public void SetEmailReferences(ModelBuilder modelBuilder)
        {
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
