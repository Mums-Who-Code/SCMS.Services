// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Guardians.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public partial class GuardianRequestProcessingService : IGuardianRequestProcessingService
    {
        private delegate ValueTask<GuardianRequest> ReturningGuardianRequestFunction();

        private async ValueTask<GuardianRequest> TryCatch(
            ReturningGuardianRequestFunction returningGuardianRequestFunction)
        {
            try
            {
                return await returningGuardianRequestFunction();
            }
            catch (NullGuardianRequestProcessingException nullGuardianRequestProcessingException)
            {
                throw CreateAndLogValidationException(nullGuardianRequestProcessingException);
            }
            catch (InvalidGuardianRequestProcessingException invalidGuardianRequestProcessingException)
            {
                throw CreateAndLogValidationException(invalidGuardianRequestProcessingException);
            }
            catch (GuardianValidationException guardianValidationException)
            {
                throw CreateAndLogDependencyValidationException(guardianValidationException);
            }
            catch (GuardianDependencyException guardianDependencyException)
            {
                throw CreateAndLogDependencyException(guardianDependencyException);
            }
            catch (GuardianServiceException guardianServiceException)
            {
                throw CreateAndLogDependencyException(guardianServiceException);
            }
            catch(Exception exception)
            {
                var failedGuardianRequestProcessingException =
                    new FailedGuardianRequestProcessingException(exception);

                throw CreateAndLogServiceException(failedGuardianRequestProcessingException);
            }
        }

        private GuardianRequestProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianRequestProcessingValidationException =
                new GuardianRequestProcessingValidationException(exception);

            this.loggingBroker.LogError(guardianRequestProcessingValidationException);

            return guardianRequestProcessingValidationException;
        }

        private GuardianRequestProcessingDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var guardianRequestProcessingDependencyValidationException =
                new GuardianRequestProcessingDependencyValidationException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(guardianRequestProcessingDependencyValidationException);

            return guardianRequestProcessingDependencyValidationException;
        }

        private GuardianRequestProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var guardianRequestProcessingDependencyException =
                new GuardianRequestProcessingDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(guardianRequestProcessingDependencyException);

            return guardianRequestProcessingDependencyException;
        }

        private GuardianRequestProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var guardianRequestProcessingServiceException =
                new GuardianRequestProcessingServiceException(exception);

            this.loggingBroker.LogError(guardianRequestProcessingServiceException);

            return guardianRequestProcessingServiceException;
        }
    }
}
