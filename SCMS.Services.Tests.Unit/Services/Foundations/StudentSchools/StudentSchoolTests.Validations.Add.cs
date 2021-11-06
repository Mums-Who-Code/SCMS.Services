// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentSchools;
using SCMS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
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
                this.studentSchoolService.AddStudentSchoolAsync(nullStudentSchool);

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

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidId = Guid.Empty;
            var invalidStudentSchool = new StudentSchool();

            var invalidStudentSchoolException =
                new InvalidStudentSchoolException();

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.StudentId),
                values: "Id is required.");

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.SchoolId),
                values: "Id is required.");

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.CreatedDate),
                values: "Date is required.");

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.CreatedBy),
                values: "Id is required.");

            var expectedStudentSchoolValidationException =
                new StudentSchoolValidationException(
                    invalidStudentSchoolException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchoolAsync(invalidStudentSchool);

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

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            int minutes = GetRandomNumber();
            StudentSchool randomStudentSchool = CreateRandomStudentSchool();
            StudentSchool invalidStudentSchool = randomStudentSchool;

            invalidStudentSchool.UpdatedDate =
                invalidStudentSchool.CreatedDate.AddMinutes(minutes);

            var invalidStudentSchoolException = new InvalidStudentSchoolException();

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.UpdatedDate),
                values: $"Date is not same as {nameof(StudentSchool.CreatedDate)}");

            var expectedStudentSchoolValidationException =
                new StudentSchoolValidationException(invalidStudentSchoolException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchoolAsync(invalidStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolValidationException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsUpdatedByAndLogItAsync()
        {
            // given
            Guid randomGuid = Guid.NewGuid();
            StudentSchool randomStudentSchool = CreateRandomStudentSchool();
            StudentSchool invalidStudentSchool = randomStudentSchool;
            invalidStudentSchool.UpdatedBy = randomGuid;
            var invalidStudentSchoolException = new InvalidStudentSchoolException();

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.UpdatedBy),
                values: $"Id is not same as {nameof(StudentSchool.CreatedBy)}");

            var expectedStudentSchoolValidationException =
                new StudentSchoolValidationException(invalidStudentSchoolException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchoolAsync(invalidStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolValidationException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
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
            StudentSchool randomStudentSchool = CreateRandomStudentSchool(dates: randomDateTime);
            StudentSchool invalidStudentSchool = randomStudentSchool;

            invalidStudentSchool.CreatedDate =
                invalidStudentSchool.CreatedDate.AddMinutes(invalidMinutes);

            var invalidStudentSchoolException = new InvalidStudentSchoolException();

            invalidStudentSchoolException.AddData(
                key: nameof(StudentSchool.CreatedDate),
                values: "Date is not recent");

            var expectedStudentSchoolValidationException =
                new StudentSchoolValidationException(invalidStudentSchoolException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchoolAsync(invalidStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolValidationException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
