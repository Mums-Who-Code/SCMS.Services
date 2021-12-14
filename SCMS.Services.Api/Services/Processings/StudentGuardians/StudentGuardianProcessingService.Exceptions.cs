// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService
    {
        private delegate ValueTask<StudentGuardian> ReturningStudentGuardianFunction();

        private async ValueTask<StudentGuardian> TryCatch(ReturningStudentGuardianFunction
            returningStudentGuardianFunction)
        {
            try
            {
                return await returningStudentGuardianFunction();
            }
            catch (NullStudentGuardianProcessingException nullStudentGuardianProcessingException)
            {
                throw CreateAndLogValidationException(nullStudentGuardianProcessingException);
            }
            catch (InvalidStudentGuardianProcessingException invalidStudentGuardianProcessingException)
            {
                throw CreateAndLogValidationException(invalidStudentGuardianProcessingException);
            }
            catch (AlreadyExistsPrimaryStudentGuardianProcessingException
                alreadyExistsPrimaryStudentGuardianProcessingException)
            {
                throw CreateAndLogValidationException(alreadyExistsPrimaryStudentGuardianProcessingException);
            }
            catch (StudentGuardianDependencyException studentGuardianDependencyException)
            {
                throw CreateAndLogDependencyException(studentGuardianDependencyException);
            }
            catch (StudentGuardianServiceException studentGuardianServiceException)
            {
                throw CreateAndLogDependencyException(studentGuardianServiceException);
            }
            catch (StudentGuardianValidationException studentGuardianValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentGuardianValidationException);
            }
            catch (StudentGuardianDependencyValidationException studentGuardianDependencyValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentGuardianDependencyValidationException);
            }
            catch (Exception exception)
            {
                var failedStudentGuardianProcessingServiceException =
                    new FailedStudentGuardianProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedStudentGuardianProcessingServiceException);
            }
        }

        private StudentGuardianProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingValidationException);

            return studentGuardianProcessingValidationException;
        }

        private StudentGuardianProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentGuardianProcessingDependencyException =
                new StudentGuardianProcessingDependencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentGuardianProcessingDependencyException);

            return studentGuardianProcessingDependencyException;
        }

        private StudentGuardianProcessingDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var studentGuardianProcessingDependencyValidationException =
                new StudentGuardianProcessingDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentGuardianProcessingDependencyValidationException);

            return studentGuardianProcessingDependencyValidationException;
        }

        private StudentGuardianProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var studentGuardianProcessingServiceException =
                new StudentGuardianProcessingServiceException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingServiceException);

            return studentGuardianProcessingServiceException;
        }
    }
}
