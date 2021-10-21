// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<School> InsertSchoolAsync(School school);
    }
}
