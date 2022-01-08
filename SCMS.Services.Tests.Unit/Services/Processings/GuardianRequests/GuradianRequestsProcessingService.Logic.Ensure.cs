// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        [Fact]
        public async Task ShouldEnsureGuardianRequestExistsIfGuardianExists()
        {
            // given
            GuardianRequest randomGuardianRequest = CreateRandomGuardianRequest();
            GuardianRequest inputGuardianRequest = randomGuardianRequest;
            GuardianRequest expectedGuardianRequest = inputGuardianRequest.DeepClone();
            var inputGuardianId = randomGuardianRequest.Id;

            var returningGuardian = new Guardian
            {
                Id = randomGuardianRequest.Id,
                Title = (Title)randomGuardianRequest.Title,
                FirstName = randomGuardianRequest.FirstName,
                LastName = randomGuardianRequest.LastName,
                EmailId = randomGuardianRequest.EmailId,
                CountryCode = randomGuardianRequest.CountryCode,
                ContactNumber = randomGuardianRequest.ContactNumber,
                Occupation = randomGuardianRequest.Occupation,
                CreatedBy = randomGuardianRequest.CreatedBy,
                UpdatedBy = randomGuardianRequest.UpdatedBy,
                CreatedDate = randomGuardianRequest.CreatedDate,
                UpdatedDate = randomGuardianRequest.UpdatedDate
            };

            this.guardianServiceMock.Setup(service =>
                service.RetrieveGuardianByIdAsync(inputGuardianId))
                    .ReturnsAsync(returningGuardian);

            // when
            GuardianRequest actualGuardianRequest =
                await this.guardianRequestProcessingService
                    .EnsureGuardianRequestExists(
                        inputGuardianRequest);

            // then
            actualGuardianRequest.Should().BeEquivalentTo(expectedGuardianRequest);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(inputGuardianId),
                    Times.Once());

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEnsureGuardianRequestExistsIfGuardianIsNotFound()
        {
            // given
            Guardian noGuardian = null;
            GuardianRequest randomGuardianRequest = CreateRandomGuardianRequest();
            GuardianRequest inputGuardianRequest = randomGuardianRequest;
            GuardianRequest expectedGuardianRequest = inputGuardianRequest.DeepClone();
            var inputGuardianId = randomGuardianRequest.Id;

            this.guardianServiceMock.Setup(service =>
                service.RetrieveGuardianByIdAsync(inputGuardianId))
                    .ReturnsAsync(noGuardian);

            // when
            GuardianRequest actualGuardianRequest =
                await this.guardianRequestProcessingService
                    .EnsureGuardianRequestExists(
                        inputGuardianRequest);

            // then
            actualGuardianRequest.Should().BeEquivalentTo(expectedGuardianRequest);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(inputGuardianId),
                    Times.Once());

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
