// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.Phones
{
    public partial class PhoneService
    {
        private static void ValidatePhone(Phone phone)
        {
            if (phone == null)
            {
                throw new NullPhoneException();
            }
        }
    }
}
