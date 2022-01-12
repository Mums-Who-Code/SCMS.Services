// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
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
        }

        private GuardianRequestProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var guardianRequestProcessingValidationException =
                new GuardianRequestProcessingValidationException(exception);

            this.loggingBroker.LogError(guardianRequestProcessingValidationException);

            return guardianRequestProcessingValidationException;
        }
    }
}
