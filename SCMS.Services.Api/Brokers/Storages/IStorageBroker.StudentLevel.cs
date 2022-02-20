// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
    DbSet<StudentLevel> InsertStudentLevelAsync(StudentLevel studentLevel);
    }
}
