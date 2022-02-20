// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentLevels;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentLevels
{
    public partial class StudentLevelServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentLevelAsync()
        {
            // given
            StudentLevel randomStudentLevel = CreateRandomStudentLevel();
            StudentLevel inputStudentLevel = randomStudentLevel;
            StudentLevel storageStudentLevel = inputStudentLevel;
            StudentLevel expectedStudentLevel = storageStudentLevel.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentLevelAsync(inputStudentLevel))
                    .ReturnsAsync(storageStudentLevel);

            // when
            StudentLevel actualStudentLevel = await this.studentLevelService
                    .AddStudentLevelAsync(inputStudentLevel);

            // then
            actualStudentLevel.Should().BeEquivalentTo(expectedStudentLevel);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentLevelAsync(inputStudentLevel),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
