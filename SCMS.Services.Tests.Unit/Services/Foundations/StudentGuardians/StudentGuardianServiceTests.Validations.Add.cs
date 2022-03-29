// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentGuardianIsNullAndLogItAsync()
        {
            // given
            StudentGuardian nullStudentGuardian = null;

            var nullStudentGuardianException =
                new NullStudentGuardianException();

            var expectedStudentGuardianValidationException =
                new StudentGuardianValidationException(nullStudentGuardianException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(nullStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentGuardianIsInvalidAndLogItAsync()
        {
            // given
            var invalidStudentGuardian = new StudentGuardian();
            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.StudentId),
                values: "Id is required.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.GuardianId),
                values: "Id is required.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.CreatedBy),
                values: "Id is required.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.CreatedDate),
                values: "Date is required.");

            var expectedStudentGuardianValidationException =
                new StudentGuardianValidationException(invalidStudentGuardianException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDate = GetRandomDateTime();
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian(randomDate);
            StudentGuardian invalidStudentGuardian = randomStudentGuardian;
            invalidStudentGuardian.UpdatedDate = invalidStudentGuardian.CreatedDate.AddDays(1);
            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.CreatedDate),
                values: $"Date is not same as {nameof(StudentGuardian.UpdatedDate)}.");

            var expectedStudentGuardianValidationException =
                new StudentGuardianValidationException(invalidStudentGuardianException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDate);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsUpdatedByAndLogItAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian invalidStudentGuardian = randomStudentGuardian;
            invalidStudentGuardian.UpdatedBy = Guid.NewGuid();
            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.CreatedBy),
                values: $"Id is not same as {nameof(StudentGuardian.UpdatedBy)}.");

            var expectedStudentGuardianValidationException =
                new StudentGuardianValidationException(invalidStudentGuardianException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsInvalidAndLogItAsync(
            int invalidMinutes)
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian(dates: randomDateTime);
            StudentGuardian invalidStudentGuardian = randomStudentGuardian;

            invalidStudentGuardian.CreatedDate =
                invalidStudentGuardian.CreatedDate.AddMinutes(invalidMinutes);

            invalidStudentGuardian.UpdatedDate = invalidStudentGuardian.CreatedDate;
            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.CreatedDate),
                values: "Date is not recent");

            var expectedStudentGuardianValidationException =
                new StudentGuardianValidationException(invalidStudentGuardianException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
