// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Phones
{
    public partial class PhoneServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Phone somePhone = CreateRandomPhone();
            SqlException sqlException = GetSqlException();

            var failedPhoneStorageException =
                new FailedPhoneStorageException(sqlException);

            var expectedPhoneDependencyException =
                new PhoneDependencyException(failedPhoneStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(somePhone);

            // then
            await Assert.ThrowsAsync<PhoneDependencyException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPhoneDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
