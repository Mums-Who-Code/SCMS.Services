// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Models.Foundations.Schools.Exceptions;
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
    }
}
