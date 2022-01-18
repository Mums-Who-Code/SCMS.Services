// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using Xeptions;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationService : IStudentGuardianRequestOrchestrationService
    {
        private delegate ValueTask<GuardianRequest> ReturningGuardianRequestFunction();

        private async ValueTask<GuardianRequest> TryCatch(
            ReturningGuardianRequestFunction returningGuardianRequestFunction)
        {
            try
            {
                return await returningGuardianRequestFunction();
            }
            catch (NullStudentGuardianRequestOrchestrationException
                nullStudentGuardianRequestOrchestrationException)
            {
                throw CreateAndLogValidationException(
                    nullStudentGuardianRequestOrchestrationException);
            }
        }

        private StudentGuardianRequestOrchestrationValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var studentGuardianRequestOrchestrationValidationException =
                new StudentGuardianRequestOrchestrationValidationException(exception);

            this.loggingBroker.LogError(studentGuardianRequestOrchestrationValidationException);

            return studentGuardianRequestOrchestrationValidationException;
        }
    }
}
