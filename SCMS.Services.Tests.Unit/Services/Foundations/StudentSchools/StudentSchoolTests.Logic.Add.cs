// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolTests
    {
        [Fact]
        public async void ShouldAddStudentSchoolAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();

            StudentSchool randomStudentSchool =
                CreateRandomStudentSchool(
                    dates: randomDateTime);

            StudentSchool inputStudentSchool =
                randomStudentSchool;

            StudentSchool storageStudentSchool =
                inputStudentSchool;

            StudentSchool expectedStudentSchool =
                storageStudentSchool.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentSchoolAsync(inputStudentSchool))
                    .ReturnsAsync(storageStudentSchool);

            // when
            StudentSchool actualStudentSchool =
                await this.studentSchoolService
                    .AddStudentSchoolAsync(inputStudentSchool);

            // then
            actualStudentSchool.Should()
                .BeEquivalentTo(expectedStudentSchool);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(
                    It.IsAny<StudentSchool>()),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
