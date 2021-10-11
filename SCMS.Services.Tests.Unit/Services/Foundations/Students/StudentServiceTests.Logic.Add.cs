// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SMCS.Services.Api.Models.Foundations.Students;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentAsync()
        {
            // given
            Student randomStudent =
                CreateRandomStudent();

            Student inputStudent = randomStudent;
            Student storedStudent = inputStudent;

            Student expectedStudent =
                storedStudent.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentAsync(inputStudent))
                    .ReturnsAsync(storedStudent);

            // when
            Student actualStudent = await
                this.studentService.AddStudentAsync(
                    inputStudent);

            // then
            actualStudent.Should().
                BeEquivalentTo(expectedStudent);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentAsync(
                    It.IsAny<Student>()),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
