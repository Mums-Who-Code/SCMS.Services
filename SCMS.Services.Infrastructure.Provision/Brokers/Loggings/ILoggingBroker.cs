// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogActivity(string message);
    }
}
