// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnEnsureIfGuardianRequestIsNullAndLogItAsync()
        {
            // given
            GuardianRequest nullGuardianRequest = null;

            var nullGuardianRequestProcessingException =
                new NullGuardianRequestProcessingException();

            var expectedGuardianRequestValidationException =
                new GuardianRequestProcessingValidationException(
                    nullGuardianRequestProcessingException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExists(
                        nullGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingValidationException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Once);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
