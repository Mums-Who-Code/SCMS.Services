// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Schools;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Foundations.Schools
{
    public interface ISchoolService
    {
        ValueTask<School> AddSchoolAsync(School school);
        IQueryable<School> RetrieveAllSchools();
    }
}
