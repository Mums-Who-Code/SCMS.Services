// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Users;
using System;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            var serviceAdminUser = new User
            {
                Id = Guid.Parse("DDBDA33E-4DF4-44CA-945D-62FEC7F73973"),
                Name = "Admin"
            };

            modelBuilder.Entity<User>().HasData(serviceAdminUser);
        }
    }
}
