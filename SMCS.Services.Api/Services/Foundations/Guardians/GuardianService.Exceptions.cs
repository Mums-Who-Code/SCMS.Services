// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Guardians;
using SMCS.Services.Api.Models.Foundations.Guardians.Exceptions;
using Xeptions;

namespace SMCS.Services.Api.Services.Foundations.Guardians
{
    public partial class GuardianService
    {
        private delegate ValueTask<Guardian> ReturningGuardianFunction();

        private async ValueTask<Guardian> TryCatch(ReturningGuardianFunction returningGuardianFunction)
        {
            try
            {
                return await returningGuardianFunction();
            }
            catch (NullGuardianException nullGuardianException)
            {
                throw CreateAndLogValidationException(nullGuardianException);
            }
            catch (InvalidGuardianException invalidGuardianException)
            {
                throw CreateAndLogValidationException(invalidGuardianException);
            }
            catch (SqlException sqlException)
            {
                var failedGuardianStorageException =
                    new FailedGuardianStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedGuardianStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsGuardianException =
                    new AlreadyExistsGuardianException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsGuardianException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidGuardianReferenceException =
                    new InvalidGuardianReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(
                    invalidGuardianReferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedGuardianStorageException =
                    new FailedGuardianStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(
                    failedGuardianStorageException);
            }
            catch (Exception exception)
            {
                var failedGuardianServiceException =
                    new FailedGuardianServiceException(exception);

                throw CreateAndLogServiceException(
                    failedGuardianServiceException);
            }
        }

        private GuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianValidationException = new GuardianValidationException(exception);
            this.loggingBroker.LogError(guardianValidationException);

            return guardianValidationException;
        }

        private GuardianDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var guardianDependencyException = new GuardianDependencyException(exception);
            this.loggingBroker.LogCritical(guardianDependencyException);

            return guardianDependencyException;
        }

        private GuardianDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guardianDependencyValidationException = new GuardianDependencyValidationException(exception);
            this.loggingBroker.LogError(guardianDependencyValidationException);

            return guardianDependencyValidationException;
        }

        private GuardianDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var guardianDependencyException = new GuardianDependencyException(exception);
            this.loggingBroker.LogError(guardianDependencyException);

            return guardianDependencyException;
        }

        private GuardianServiceException CreateAndLogServiceException(Xeption exception)
        {
            var guardianServiceException = new GuardianServiceException(exception);
            this.loggingBroker.LogError(guardianServiceException);

            return guardianServiceException;
        }
    }
}
