// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
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
        }

        private GuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentValidationException = new GuardianValidationException(exception);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }
    }
}
