// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCMS.Services.Api.Models.Foundations.Emails;
using SCMS.Services.Api.Models.Foundations.Guardians;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Email> Emails { get; set; }
    }
}
