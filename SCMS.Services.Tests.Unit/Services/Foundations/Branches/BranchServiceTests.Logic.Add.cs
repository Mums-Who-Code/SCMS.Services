// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Branches;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Branches
{
    public partial class BranchServiceTests
    {
        [Fact]
        public async Task ShouldAddBranchAsync()
        {
            // given
            Branch randomBranch = CreateRandomBranch();
            Branch inputBranch = randomBranch;
            Branch storageBranch = inputBranch;
            Branch expectedBranch = storageBranch.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBranchAsync(inputBranch))
                    .ReturnsAsync(storageBranch);

            // when
            Branch actualBranch = await this.branchService
                    .AddBranchAsync(inputBranch);

            // then
            actualBranch.Should().BeEquivalentTo(expectedBranch);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBranchAsync(inputBranch),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
