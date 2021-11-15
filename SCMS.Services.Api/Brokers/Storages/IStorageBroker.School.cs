// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------


using System;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Schools;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<School> InsertSchoolAsync(School school);
        IQueryable<School> SelectAllSchools();
        ValueTask<School> SelectSchoolByIdAsync(Guid schoolId);
        ValueTask<School> UpdateSchoolAsync(School school);
    }
}
