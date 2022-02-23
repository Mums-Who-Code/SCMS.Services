// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionService
    {
        private delegate ValueTask<TermsAndCondition> ReturningTermsAndConditionFunction();

        private async ValueTask<TermsAndCondition> TryCatch(
            ReturningTermsAndConditionFunction returningTermsAndConditionFunction)
        {
            try
            {
                return await returningTermsAndConditionFunction();
            }
            catch (NullTermsAndConditionException nullTermsAndConditionException)
            {
                throw CreateAndLogValidationException(nullTermsAndConditionException);
            }
        }

        private TermsAndConditionValidationException CreateAndLogValidationException(Xeption exception)
        {
            var termsAndConditionValidationException =
                new TermsAndConditionValidationException(exception);

            this.loggingBroker.LogError(termsAndConditionValidationException);

            return termsAndConditionValidationException;
        }
    }
}
