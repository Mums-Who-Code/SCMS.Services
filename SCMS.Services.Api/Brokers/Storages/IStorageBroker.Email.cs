// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Emails;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Email> InsertEmailAsync(Email email);
        IQueryable<Email> SelectAllEmails();
    }
}
