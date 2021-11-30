// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Phone> InsertPhoneAsync(Phone phone);
        IQueryable<Phone> SelectAllPhones();
        ValueTask<Phone> SelectPhoneByIdAsync(Guid phoneId);
    }
}
