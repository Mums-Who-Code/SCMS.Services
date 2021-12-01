// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Occupations;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Occupation> Occupations { get; set; }
    }
}
