// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentSchoolIsNullAndLogItAsync()
        {
            // given
            StudentSchool nullStudentSchool = null;

            var nullStudentSchoolException =
                new NullStudentSchoolException();

            var expectedStudentSchoolValidationException =
                new StudentSchoolValidationException(
                    nullStudentSchoolException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchool(nullStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolValidationException>(() =>
                addStudentSchoolTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
