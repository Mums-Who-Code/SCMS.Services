// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
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
            var invalidStudentGuardian = new StudentGuardian
            {
                Relation = GetInvalidEnum<Relationship>(),
                Level = GetInvalidEnum<ContactLevel>()
            };

            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.StudentId),
                values: "Id is required.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.GuardianId),
                values: "Id is required.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.Relation),
                values: "Value is invalid.");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.Level),
                values: "Value is invalid.");

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
    }
}
