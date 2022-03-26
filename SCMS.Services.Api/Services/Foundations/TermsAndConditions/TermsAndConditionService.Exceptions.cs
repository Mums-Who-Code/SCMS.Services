// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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
            catch (InvalidTermsAndConditionException invalidTermsAndConditionException)
            {
                throw CreateAndLogValidationException(invalidTermsAndConditionException);
            }
            catch (SqlException sqlException)
            {
                var failedTermsAndConditionStorageException =
                    new FailedTermsAndConditionStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedTermsAndConditionStorageException);
            }
        }

        private TermsAndConditionValidationException CreateAndLogValidationException(Xeption exception)
        {
            var termsAndConditionValidationException =
                new TermsAndConditionValidationException(exception);

            this.loggingBroker.LogError(termsAndConditionValidationException);

            return termsAndConditionValidationException;
        }

        private TermsAndConditionDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var termsAndConditionDependencyException =
                new TermsAndConditionDependencyException(exception);

            this.loggingBroker.LogCritical(termsAndConditionDependencyException);

            return termsAndConditionDependencyException;
        }
    }
}
