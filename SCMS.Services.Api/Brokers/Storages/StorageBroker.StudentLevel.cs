// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Brokers.Storages
{
    public  partial class StorageBroker
    {
        DbSet<StudentLevel> StudentLevels { get; set; }
    }
}
