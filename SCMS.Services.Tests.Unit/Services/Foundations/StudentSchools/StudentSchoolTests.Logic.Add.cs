// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
            StudentSchool randomStudentSchool =
                CreateRandomStudentSchool();

            StudentSchool inputStudentSchool =
                randomStudentSchool;

            StudentSchool storageStudentSchool =
                inputStudentSchool;

            StudentSchool expectedStudentSchool =
                storageStudentSchool.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentSchoolAsync(inputStudentSchool))
                    .ReturnsAsync(storageStudentSchool);

            // when
            StudentSchool actualStudentSchool =
                await this.studentSchoolService
                    .AddStudentSchool(inputStudentSchool);

            // then
            actualStudentSchool.Should()
                .BeEquivalentTo(expectedStudentSchool);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(
                    It.IsAny<StudentSchool>()),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
