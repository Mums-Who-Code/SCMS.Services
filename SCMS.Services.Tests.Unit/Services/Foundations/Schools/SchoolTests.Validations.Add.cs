// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.Schools.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSchoolIsNullAndLogItAsync()
        {
            // given
            School nullSchool = null;

            var nullSchoolException =
                new NullSchoolException();

            var expectedSchoolValidationException =
                new SchoolValidationException(nullSchoolException);

            // when
            ValueTask<School> addSchoolTask =
                this.schoolService.AddSchoolAsync(nullSchool);

            // then
            await Assert.ThrowsAsync<SchoolValidationException>(() =>
                addSchoolTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                        expectedSchoolValidationException))),
                            Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(It.IsAny<School>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIsInvalidAndLogItAsync(
            string invalidName)
        {
            // given
            var invalidSchool = new School
            {
                Name = invalidName
            };

            var invalidSchoolException = new InvalidSchoolException();

            invalidSchoolException.AddData(
                key: nameof(School.Id),
                values: "Id is required.");

            invalidSchoolException.AddData(
                key: nameof(School.Name),
                values: "Text is required.");

            invalidSchoolException.AddData(
                key: nameof(School.CreatedDate),
                values: "Date is required.");

            invalidSchoolException.AddData(
                key: nameof(School.UpdatedDate),
                values: "Date is required.");

            invalidSchoolException.AddData(
                key: nameof(School.CreatedBy),
                values: "Id is required.");

            invalidSchoolException.AddData(
                key: nameof(School.UpdatedBy),
                values: "Id is required.");

            var expectedSchoolValidationException =
                new SchoolValidationException(invalidSchoolException);

            // when
            ValueTask<School> addSchoolTask =
                this.schoolService.AddSchoolAsync(invalidSchool);

            // then
            await Assert.ThrowsAsync<SchoolValidationException>(() =>
                addSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(It.IsAny<School>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            int minutes = GetRandomNumber();
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            School randomSchool = CreateRandomSchool();
            School invalidSchool = randomSchool;

            invalidSchool.UpdatedDate =
                invalidSchool.CreatedDate.AddMinutes(minutes);

            var invalidSchoolException = new InvalidSchoolException();

            invalidSchoolException.AddData(
                key: nameof(School.UpdatedDate),
                values: $"Date is not same as {nameof(School.CreatedDate)}");

            var expectedSchoolValidationException =
                new SchoolValidationException(invalidSchoolException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<School> addSchoolTask =
                this.schoolService.AddSchoolAsync(invalidSchool);

            // then
            await Assert.ThrowsAsync<SchoolValidationException>(() =>
                addSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(It.IsAny<School>()),
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
            School randomSchool = CreateRandomSchool();
            School invalidSchool = randomSchool;
            invalidSchool.UpdatedBy = randomGuid;
            var invalidSchoolException = new InvalidSchoolException();

            invalidSchoolException.AddData(
                key: nameof(School.UpdatedBy),
                values: $"Id is not same as {nameof(School.CreatedBy)}");

            var expectedSchoolValidationException =
                new SchoolValidationException(invalidSchoolException);

            // when
            ValueTask<School> addSchoolTask =
                this.schoolService.AddSchoolAsync(invalidSchool);

            // then
            await Assert.ThrowsAsync<SchoolValidationException>(() =>
                addSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(It.IsAny<School>()),
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
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            School randomSchool = CreateRandomSchool(dates: randomDateTime);
            School invalidSchool = randomSchool;

            invalidSchool.CreatedDate =
                invalidSchool.CreatedDate.AddMinutes(invalidMinutes);

            var invalidSchoolException = new InvalidSchoolException();

            invalidSchoolException.AddData(
                key: nameof(School.CreatedDate),
                values: "Date is not recent");

            var expectedSchoolValidationException =
                new SchoolValidationException(invalidSchoolException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<School> addSchoolTask =
                this.schoolService.AddSchoolAsync(invalidSchool);

            // then
            await Assert.ThrowsAsync<SchoolValidationException>(() =>
                addSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSchoolValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(It.IsAny<School>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
