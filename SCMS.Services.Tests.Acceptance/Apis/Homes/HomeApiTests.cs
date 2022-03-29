// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using SCMS.Services.Tests.Acceptance.Brokers;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Acceptance.Apis.Homes
{
    [Collection(nameof(ServicesApiTestCollection))]
    public class HomeApiTests
    {
        private readonly ServicesApiBroker servicesApiBroker;

        public HomeApiTests(ServicesApiBroker servicesApiBroker) =>
            this.servicesApiBroker = servicesApiBroker;

        [Fact]
        public async Task ShouldGetHomeMessage()
        {
            // given
            string expectedMessage = "Hello from SCMS Services Api.";

            // when
            string actualMessage = await this.servicesApiBroker.GetHomeMessageAsync();

            // then
            actualMessage.Should().BeEquivalentTo(expectedMessage);
        }
    }
}
