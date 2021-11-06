// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Services.Api.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTime() =>
            DateTime.UtcNow;
    }
}
