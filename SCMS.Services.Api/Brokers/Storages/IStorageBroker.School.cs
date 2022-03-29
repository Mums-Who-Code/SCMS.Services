// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------


using SCMS.Services.Api.Models.Foundations.Schools;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<School> InsertSchoolAsync(School school);
        IQueryable<School> SelectAllSchools();
        ValueTask<School> SelectSchoolByIdAsync(Guid schoolId);
        ValueTask<School> UpdateSchoolAsync(School school);
        ValueTask<School> DeleteSchoolAsync(School school);
    }
}
