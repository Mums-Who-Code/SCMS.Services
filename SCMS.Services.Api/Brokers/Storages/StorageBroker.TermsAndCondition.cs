// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TermsAndCondition> TermsAndConditions { get; set; }
    }
}
