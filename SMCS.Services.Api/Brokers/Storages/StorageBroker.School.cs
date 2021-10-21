// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<School> Schools { get; set; }
    }
}
