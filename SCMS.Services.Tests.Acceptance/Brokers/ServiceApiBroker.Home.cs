// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace SCMS.Services.Tests.Acceptance.Brokers
{
    public partial class ServicesApiBroker
    {
        private const string HomesRelativeUrl = "api/home";

        public async ValueTask<string> GetHomeMessageAsync() =>
            await this.apiFactoryClient.GetContentStringAsync(HomesRelativeUrl);
    }
}
