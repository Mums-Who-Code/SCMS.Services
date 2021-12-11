// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

namespace SCMS.Services.Infrastructure.Provision.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}
