// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Models.Foundations.Agreements.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.Agreements
{
    public partial class AgreementService
    {
        private delegate ValueTask<Agreement> RturningAgreementFunction();

        private async ValueTask<Agreement> TryCatch(RturningAgreementFunction rturningAgreementFunction)
        {
            try
            {
                return await rturningAgreementFunction();
            }
            catch (NullAgreementException nullAgreementException)
            {
                throw CreateAndLogValidationException(nullAgreementException);
            }
        }

        private AgreementValidationException CreateAndLogValidationException(Xeption exception)
        {
            var agreementValidationException =
                new AgreementValidationException(exception);

            this.loggingBroker.LogError(agreementValidationException);

            return agreementValidationException;
        }
    }
}
