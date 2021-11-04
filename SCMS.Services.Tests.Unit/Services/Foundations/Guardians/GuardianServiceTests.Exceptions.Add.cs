// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moq;
using SMCS.Services.Api.Models.Foundations.Guardians;
using SMCS.Services.Api.Models.Foundations.Guardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guardian someGuardian = CreateRandomGuardian();
            SqlException sqlException = GetSqlException();

            var failedGuardianStorageException =
                new FailedGuardianStorageException(sqlException);

            var expectedGuardianDependencyException =
                new GuardianDependencyException(failedGuardianStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGuardianDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
