// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.Phones
{
    public partial class PhoneService
    {
        private delegate ValueTask<Phone> ReturningPhoneFunction();

        private async ValueTask<Phone> TryCatch(ReturningPhoneFunction returningPhoneFunction)
        {
            try
            {
                return await returningPhoneFunction();
            }
            catch (NullPhoneException nullPhoneException)
            {
                throw CreateAndLogValidationException(nullPhoneException);
            }
            catch (InvalidPhoneException invalidPhoneException)
            {
                throw CreateAndLogValidationException(invalidPhoneException);
            }
            catch (SqlException sqlException)
            {
                var failedPhoneStorageException =
                    new FailedPhoneStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedPhoneStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsPhoneException =
                    new AlreadyExistsPhoneException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsPhoneException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidPhoneReferenceException =
                    new InvalidPhoneReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidPhoneReferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedPhoneStorageException =
                    new FailedPhoneStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedPhoneStorageException);
            }
            catch (Exception exception)
            {
                var failedPhoneServiceException =
                    new FailedPhoneServiceException(exception);

                throw CreateAndLogServiceException(failedPhoneServiceException);
            }
        }

        private PhoneValidationException CreateAndLogValidationException(Xeption exception)
        {
            var phoneValidationException = new PhoneValidationException(exception);
            this.loggingBroker.LogError(phoneValidationException);

            return phoneValidationException;
        }

        private PhoneDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var phoneDependencyException = new PhoneDependencyException(exception);
            this.loggingBroker.LogCritical(phoneDependencyException);

            return phoneDependencyException;
        }

        private PhoneDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var phoneDependencyValidationException = new PhoneDependencyValidationException(exception);
            this.loggingBroker.LogError(phoneDependencyValidationException);

            return phoneDependencyValidationException;
        }

        private PhoneDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var phoneDependencyException = new PhoneDependencyException(exception);
            this.loggingBroker.LogError(phoneDependencyException);

            return phoneDependencyException;
        }

        private PhoneServiceException CreateAndLogServiceException(Xeption exception)
        {
            var phoneServiceException = new PhoneServiceException(exception);
            this.loggingBroker.LogError(phoneServiceException);

            return phoneServiceException;
        }
    }
}
