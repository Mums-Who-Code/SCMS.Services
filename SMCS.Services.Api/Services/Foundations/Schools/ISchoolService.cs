// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Services.Foundations.Schools
{
    public interface ISchoolService
    {
        ValueTask<School> AddSchoolAsync(School school);
    }
}
