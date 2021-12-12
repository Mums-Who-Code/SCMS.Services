// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Azure.Management.Sql.Fluent;

namespace SCMS.Services.Infrastructure.Provision.Models.Storages
{
    internal class SqlDatabase
    {
        public string ConnectionString { get; set; }
        public ISqlDatabase Database { get; set; }
    }
}
