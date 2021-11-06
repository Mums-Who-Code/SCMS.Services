// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<Student> InsertStudentAsync(Student student);
    }
}
