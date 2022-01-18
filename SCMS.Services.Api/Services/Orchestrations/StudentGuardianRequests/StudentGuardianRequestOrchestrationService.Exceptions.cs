// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
            catch (StudentProcessingDependencyException
                studentProcessingDependencyException)
            {
                throw CreateAndLogDependencyException(
                    studentProcessingDependencyException);
            }
            catch (StudentProcessingServiceException
                studentProcessingServiceException)
            {
                throw CreateAndLogDependencyException(
                    studentProcessingServiceException);
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
            catch (GuardianRequestProcessingDependencyException
                guardianRequestProcessingDependencyException)
            {
                throw CreateAndLogDependencyException(
                    guardianRequestProcessingDependencyException);
            }
            catch (GuardianRequestProcessingServiceException
                guardianRequestProcessingServiceException)
            {
                throw CreateAndLogDependencyException(
                    guardianRequestProcessingServiceException);
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
            catch (StudentGuardianProcessingDependencyException
                studentGuardianProcessingDependencyException)
            {
                throw CreateAndLogDependencyException(
                    studentGuardianProcessingDependencyException);
            }
            catch (StudentGuardianProcessingServiceException
                studentGuardianProcessingServiceException)
            {
                throw CreateAndLogDependencyException(
                    studentGuardianProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedStudentGuardianRequestOrchestrationException =
                    new FailedStudentGuardianRequestOrchestrationException(exception);

                throw CreateAndLogServiceException(
                    failedStudentGuardianRequestOrchestrationException);
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

        private StudentGuardianRequestOrchestrationDependencyException
            CreateAndLogDependencyException(Xeption exception)
        {
            var studentGuardianRequestOrchestrationDependencyException =
                new StudentGuardianRequestOrchestrationDependencyException(
                    exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentGuardianRequestOrchestrationDependencyException);

            return studentGuardianRequestOrchestrationDependencyException;
        }

        private StudentGuardianRequestOrchestrationServiceException
            CreateAndLogServiceException(Xeption exception)
        {
            var studentGuardianRequestOrchestrationServiceException =
                new StudentGuardianRequestOrchestrationServiceException(
                    exception);

            this.loggingBroker.LogError(studentGuardianRequestOrchestrationServiceException);

            return studentGuardianRequestOrchestrationServiceException;
            ;
        }
    }
}
