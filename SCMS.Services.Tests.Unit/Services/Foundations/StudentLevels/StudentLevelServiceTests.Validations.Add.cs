// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]

        public async Task ShouldThrowValidationExceptionOnAddIfStudentLevelIsInvalidAndLogItAsync(string invalidName)
        {
            // given
            var invalidStudentLevel = new StudentLevel
            {
                Name = invalidName
            };

            var invalidStudentLevelException = new InvalidStudentLevelException();


            invalidStudentLevelException.AddData(
               key: nameof(StudentLevel.Name),
               values: "Name is required.");

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.CreatedDate),
                values: "Date is required.");

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.UpdatedDate),
                values: "Date is required.");

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.CreatedBy),
                values: "Id is required.");

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.UpdatedBy),
                values: "Id is required.");

            var expectedStudentLevelValidationException =
                new StudentLevelValidationException(invalidStudentLevelException);

            // when
            ValueTask<StudentLevel> addStudentLevelTask =
                this.studentLevelService.AddStudentLevelAsync(invalidStudentLevel);

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
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            StudentLevel randomStudentLevel = CreateRandomStudentLevel();
            StudentLevel invalidStudentLevel = randomStudentLevel;
            invalidStudentLevel.UpdatedDate = invalidStudentLevel.CreatedDate.AddDays(1);
            var invalidStudentLevelException = new InvalidStudentLevelException();

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.UpdatedDate),
                values: $"Date is not same as {nameof(StudentLevel.CreatedDate)}.");

            var expectedStudentLevelValidationException =
                new StudentLevelValidationException(invalidStudentLevelException);

            // when
            ValueTask<StudentLevel> addStudentLevelTask =
                this.studentLevelService.AddStudentLevelAsync(invalidStudentLevel);

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
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsByAndLogItAsync()
        {
            // given
            Guid randomGuid = Guid.NewGuid();
            StudentLevel randomStudentLevel = CreateRandomStudentLevel();
            StudentLevel invalidStudentLevel = randomStudentLevel;
            invalidStudentLevel.UpdatedBy = randomGuid;
            var invalidStudentLevelException = new InvalidStudentLevelException();

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.UpdatedBy),
                values: $"Id is not same as {nameof(StudentLevel.CreatedBy)}.");

            var expectedStudentLevelValidationException =
                new StudentLevelValidationException(invalidStudentLevelException);

            // when
            ValueTask<StudentLevel> addStudentLevelTask =
                this.studentLevelService.AddStudentLevelAsync(invalidStudentLevel);

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

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async void ShouldThrowValidationExceptionOnCreateIfCreatedDateIsNotRecentAndLogItAsync(
            int minutes)
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            StudentLevel randomStudentLevel = CreateRandomStudentLevel(dates: randomDateTime);
            StudentLevel invalidStudentLevel = randomStudentLevel;
            invalidStudentLevel.CreatedDate = invalidStudentLevel.CreatedDate.AddMinutes(minutes);
            invalidStudentLevel.UpdatedDate = invalidStudentLevel.CreatedDate;
            var invalidStudentLevelException = new InvalidStudentLevelException();

            invalidStudentLevelException.AddData(
                key: nameof(StudentLevel.CreatedDate),
                values: $"Date is not recent.");

            var expectedStudentLevelValidationException =
                new StudentLevelValidationException(invalidStudentLevelException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<StudentLevel> addStudentLevelTask =
                this.studentLevelService.AddStudentLevelAsync(invalidStudentLevel);

            // then
            await Assert.ThrowsAsync<StudentLevelValidationException>(() =>
                addStudentLevelTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentLevelValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentLevelAsync(It.IsAny<StudentLevel>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
