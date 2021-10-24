// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Models.Foundations.Schools.Exceptions;
using Xeptions;

namespace SMCS.Services.Api.Services.Foundations.Schools
{
    public partial class SchoolService
    {
        private delegate ValueTask<School> ReturningSchoolFunction();

        private async ValueTask<School> TryCatch(ReturningSchoolFunction returningSchoolFunction)
        {
            try
            {
                return await returningSchoolFunction();
            }
            catch (NullSchoolException nullSchoolException)
            {
                throw CreateAndLogValidationException(nullSchoolException);
            }
            catch (InvalidSchoolException invalidSchoolException)
            {
                throw CreateAndLogValidationException(invalidSchoolException);
            }
            catch (SqlException sqlException)
            {
                var failedSchoolStorageException =
                    new FailedSchoolStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedSchoolStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsSchoolException =
                    new AlreadyExistsSchoolException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsSchoolException);
            }
            catch(DbUpdateException databaseUpdateException)
            {
                var failedSchoolStorageException =
                    new FailedSchoolStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedSchoolStorageException);
            }
        }

        private SchoolValidationException CreateAndLogValidationException(Xeption exception)
        {
            var schoolValidationException = new SchoolValidationException(exception);
            this.loggingBroker.LogError(schoolValidationException);

            return schoolValidationException;
        }

        private SchoolDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var schoolDependencyException = new SchoolDependencyException(exception);
            this.loggingBroker.LogCritical(schoolDependencyException);

            return schoolDependencyException;
        }

        private SchoolDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var schoolDependencyValidationException = new SchoolDependencyValidationException(exception);
            this.loggingBroker.LogError(schoolDependencyValidationException);

            return schoolDependencyValidationException;
        }

        private SchoolDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var schoolDependencyException = new SchoolDependencyException(exception);
            this.loggingBroker.LogError(schoolDependencyException);

            return schoolDependencyException;
        }
    }
}
