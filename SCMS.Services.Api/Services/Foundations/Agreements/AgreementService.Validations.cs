// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Models.Foundations.Agreements.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.Agreements
{
    public partial class AgreementService
    {
        private static void ValidateInput(Agreement agreement)
        {
            if (agreement == null)
            {
                throw new NullAgreementException();
            }
        }
    }
}
