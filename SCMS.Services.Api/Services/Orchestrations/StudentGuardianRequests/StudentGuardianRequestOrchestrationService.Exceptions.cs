// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
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
            catch (InvalidStudentGuardianRequestOrchestrationException
                invalidStudentGuardianRequestOrchestrationException)
            {
                throw CreateAndLogValidationException(
                    invalidStudentGuardianRequestOrchestrationException);
            }
            catch (StudentProcessingValidationException
                studentProcessingValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    studentProcessingValidationException);
            }
            catch (StudentProcessingDependencyValidationException
                studentProcessingDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    studentProcessingDependencyValidationException);
            }
            catch (GuardianRequestProcessingValidationException
                guardianRequestProcessingValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    guardianRequestProcessingValidationException);
            }
            catch (GuardianRequestProcessingDependencyValidationException
                guardianRequestProcessingDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    guardianRequestProcessingDependencyValidationException);
            }
            catch (StudentGuardianProcessingValidationException
                studentGuardianProcessingValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    studentGuardianProcessingValidationException);
            }
            catch (StudentGuardianProcessingDependencyValidationException
                studentGuardianProcessingDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(
                    studentGuardianProcessingDependencyValidationException);
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

        private StudentGuardianRequestOrchestrationDependencyValidationException
            CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentGuardianRequestOrchestrationDependencyValidationException =
                new StudentGuardianRequestOrchestrationDependencyValidationException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentGuardianRequestOrchestrationDependencyValidationException);

            return studentGuardianRequestOrchestrationDependencyValidationException;
        }
    }
}
