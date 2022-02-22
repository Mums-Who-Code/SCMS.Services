// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentLevels;
using SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentLevels
{
    public partial class StudentLevelServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentLevelIsNullAndLogItAsync()
        {
            // given
            StudentLevel nullStudentLevel = null;
            var nullStudentLevelException = new NullStudentLevelException();

            var expectedStudentLevelValidationException =
                new StudentLevelValidationException(
                    nullStudentLevelException);

            // when
            ValueTask<StudentLevel> addStudentLevelTask =
                this.studentLevelService.AddStudentLevelAsync(nullStudentLevel);

            // then
            await Assert.ThrowsAsync<StudentLevelValidationException>(() =>
                addStudentLevelTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentLevelValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentLevelAsync(It.IsAny<StudentLevel>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
