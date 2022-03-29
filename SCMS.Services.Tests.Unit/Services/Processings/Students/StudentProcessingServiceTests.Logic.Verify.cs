// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.Students
{
    public partial class StudentProcessingServiceTests
    {
        [Fact]
        public async Task ShouldVerifyStudentExistsAsync()
        {
            // given
            Student randomStudent = CreateRandomStudent();
            Student returningStudent = randomStudent;
            Student expectedStudent = returningStudent.DeepClone();
            Guid inputStudentId = returningStudent.Id;

            this.studentServiceMock.Setup(service =>
                service.RetrieveStudentByIdAsync(inputStudentId))
                    .ReturnsAsync(returningStudent);

            // when
            Student actualStudent = await this.studentProcessingService
                .VerifyStudentExistsAsync(inputStudentId);

            // then
            actualStudent.Should().BeEquivalentTo(expectedStudent);

            this.studentServiceMock.Verify(service =>
                service.RetrieveStudentByIdAsync(inputStudentId),
                    Times.Once);

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
