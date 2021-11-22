
// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
    }
}
