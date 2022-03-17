// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            //given
            DateTimeOffset randonDateTime = GetRandomDateTime();

            TermsAndCondition randomTermsAndCondition =
                CreateRandomTermsAndCondition(randonDateTime);

            TermsAndCondition inputTermsAndCondition = randomTermsAndCondition;
            Exception sqlException = GetSqlException();

            var failedTermsAndConditionDependencyException =
                new FailedTermsAndConditionStorageException(sqlException);

            var expectedTermsAndConditionDependencyException =
                new TermsAndConditionDependencyException(
                    failedTermsAndConditionDependencyException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            //when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(
                    inputTermsAndCondition);

            //then
            await Assert.ThrowsAsync<TermsAndConditionDependencyException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTermsAndConditionDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(inputTermsAndCondition),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
