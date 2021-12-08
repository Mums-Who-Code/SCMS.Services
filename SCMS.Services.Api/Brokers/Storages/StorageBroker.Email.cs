// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Emails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Email> Emails { get; set; }

        public async ValueTask<Email> InsertEmailAsync(Email email)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Email> entityEntry =
                await broker.Emails.AddAsync(email);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<Email> SelectAllEmails() => this.Emails;
    }
}
