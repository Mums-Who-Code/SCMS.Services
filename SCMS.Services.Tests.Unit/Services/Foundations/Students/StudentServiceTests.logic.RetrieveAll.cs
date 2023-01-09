// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students;
using System.Linq;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllStudentdAsync()
        {
            //given
            IQueryable<Student> randomStudents = CreateRandomStudents();
            IQueryable<Student> retrievedStudents = randomStudents;
            IQueryable<Student> expectedStudents = retrievedStudents;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllStudents()).Returns(retrievedStudents);

            //when
            IQueryable<Student> actualStudents = this.studentService.RetrieveAllStudents();

            //then
            actualStudents.Should().BeEquivalentTo(expectedStudents);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllStudents(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
