// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TermsAndCondition> InsertTermsAndConditionAsync(TermsAndCondition termsAndCondition);
    }
}
