// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsTermsAndConditionException =
                    new AlreadyExistsTermsAndConditionException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsTermsAndConditionException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidTermsAndConditionReferenceException =
                    new InvalidTermsAndConditionReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidTermsAndConditionReferenceException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedTermsAndConditionException =
                    new FailedTermsAndConditionStorageException(dbUpdateException);

                throw CreateAndLogDependencyException(failedTermsAndConditionException);
            }
            catch (Exception exception)
            {
                var failedTermsAndConditionServiceException =
                    new FailedTermsAndConditionServiceException(exception);

                throw CreateAndLogServiceException(failedTermsAndConditionServiceException);
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

        private TermsAndConditionDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var termsAndConditionDependencyValidationException =
                new TermsAndConditionDependencyValidationException(exception);

            this.loggingBroker.LogError(termsAndConditionDependencyValidationException);

            return termsAndConditionDependencyValidationException;
        }

        private TermsAndConditionDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var termsAndConditionDependencyException =
                new TermsAndConditionDependencyException(exception);

            this.loggingBroker.LogError(termsAndConditionDependencyException);

            return termsAndConditionDependencyException;
        }

        private TermsAndConditionServiceException CreateAndLogServiceException(Xeption exception)
        {
            var termsAndConditionServiceException =
                new TermsAndConditionServiceException(exception);

            this.loggingBroker.LogError(termsAndConditionServiceException);

            return termsAndConditionServiceException;
        }
    }
}
