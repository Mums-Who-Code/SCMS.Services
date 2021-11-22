// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Phone> InsertPhoneAsync(Phone phone);
    }
}
