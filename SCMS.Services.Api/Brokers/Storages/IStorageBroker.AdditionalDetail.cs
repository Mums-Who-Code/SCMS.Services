// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.AdditionalDetails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AdditionalDetail> InsertAdditionalDetailAsync(AdditionalDetail additionalDetail);
        IQueryable<AdditionalDetail> SelectAllAdditionalDetails();
        ValueTask<AdditionalDetail> SelectAdditionalDetailByIdAsync(Guid additionalDetailId);
        ValueTask<AdditionalDetail> UpdateAdditionalDetailAsync(AdditionalDetail additionalDetail
    }
}
