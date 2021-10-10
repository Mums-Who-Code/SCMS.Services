// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xunit;

namespace SCMS.Services.Tests.Acceptance.Brokers
{
    [CollectionDefinition(nameof(ServicesApiTestCollection))]
    public class ServicesApiTestCollection : ICollectionFixture<ServicesApiBroker>
    { }
}
