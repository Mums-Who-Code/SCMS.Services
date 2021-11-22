// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Phones;

namespace SCMS.Services.Api.Services.Foundations.Phones
{
    public interface IPhoneService
    {
        ValueTask<Phone> AddPhoneAsync(Phone phone);
    }
}
