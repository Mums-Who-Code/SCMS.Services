// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<StudentGuardian> InsertStudentGuardianAsync(StudentGuardian studentGuardian);
        IQueryable<StudentGuardian> SelectAllStudentGuardians();
    }
}
