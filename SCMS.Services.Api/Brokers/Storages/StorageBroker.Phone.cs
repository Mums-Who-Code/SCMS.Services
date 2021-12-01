
// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Phone> Phones { get; set; }

        public async ValueTask<Phone> InsertPhoneAsync(Phone phone)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Phone> entityEntry =
                await broker.Phones.AddAsync(phone);

            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public IQueryable<Phone> SelectAllPhones() => this.Phones;

        public async ValueTask<Phone> SelectPhoneByIdAsync(Guid phoneId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Phones.FindAsync(phoneId);
        }

        public async ValueTask<Phone> UpdatePhoneAsync(Phone phone)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Phone> entityEntry = broker.Phones.Update(entity: phone);
            await broker.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
