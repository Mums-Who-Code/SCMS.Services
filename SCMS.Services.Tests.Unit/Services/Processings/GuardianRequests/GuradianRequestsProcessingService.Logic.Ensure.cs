// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian returningGuardian = randomGuardian;
            var inputStudentId = Guid.NewGuid();
            var inputGuardianId = randomGuardian.Id;

            var inputGuardianRequest = new GuardianRequest
            {
                Id = randomGuardian.Id,
                Title = (GuardianRequestTitle)randomGuardian.Title,
                FirstName = randomGuardian.FirstName,
                LastName = randomGuardian.LastName,
                EmailId = randomGuardian.EmailId,
                CountryCode = randomGuardian.CountryCode,
                ContactNumber = randomGuardian.ContactNumber,
                Occupation = randomGuardian.Occupation,
                StudentId = inputStudentId
            };

            var expectedGuardianRequest = inputGuardianRequest.DeepClone();

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
    }
}
