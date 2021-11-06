// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Schools;

namespace SCMS.Services.Api.Services.Foundations.Schools
{
    public interface ISchoolService
    {
        ValueTask<School> AddSchoolAsync(School school);
    }
}
