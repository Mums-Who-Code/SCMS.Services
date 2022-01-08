// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public partial class StudentProcessingService : IStudentProcessingService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (InvalidStudentProcessingException invalidStudentProcessingException)
            {
                throw CreateAndLogValidationException(invalidStudentProcessingException);
            }
            catch (NotFoundStudentProcessingException notFoundStudentProcessingException)
            {
                throw CreateAndLogValidationException(notFoundStudentProcessingException);
            }
            catch (StudentValidationException studentValidationException)
            {
                throw CreateAndLogDependencyValidationException(studentValidationException);
            }
            catch (StudentDependencyException studentDependencyException)
            {
                throw CreateAndLogDependencyException(studentDependencyException);
            }
        }

        private StudentProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentProcessingValidationException = new StudentProcessingValidationException(exception);
            this.loggingBroker.LogError(studentProcessingValidationException);

            throw studentProcessingValidationException;
        }

        private StudentProcessingDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var studentProcessingDependencyValidationException =
                new StudentProcessingDependencyValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentProcessingDependencyValidationException);

            throw studentProcessingDependencyValidationException;
        }

        private StudentProcessingDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var studentProcessingDependencyException =
                new StudentProcessingDependencyException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(studentProcessingDependencyException);

            throw studentProcessingDependencyException;
        }
    }
}
