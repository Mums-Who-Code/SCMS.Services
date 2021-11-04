// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Guardians;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Guardian> Guardians { get; set; }
    }
}
