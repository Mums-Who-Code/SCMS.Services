// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using SCMS.Services.Api;

namespace SCMS.Services.Tests.Acceptance.Brokers
{
    public partial class ServicesApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public ServicesApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.httpClient = this.webApplicationFactory.CreateClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
