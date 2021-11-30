// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.AdditionalDetails;
using SCMS.Services.Api.Models.Foundations.Phones;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AdditionalDetail> InsertAdditionalDetailAsync(AdditionalDetail additionalDetail);
    }
}
