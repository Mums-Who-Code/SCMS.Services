﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TermsAndCondition> TermsAndConditions { get; set; }

        public async ValueTask<TermsAndCondition> InsertTermsAndConditionAsync(TermsAndCondition termsAndCondition)
        {
            EntityEntry<TermsAndCondition> entityEntry =
                await this.TermsAndConditions.AddAsync(entity: termsAndCondition);

            await this.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
