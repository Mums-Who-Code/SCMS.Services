// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Branches;

namespace SCMS.Services.Api.Services.Foundations.Branches
{
    public interface IBranchService
    {
        ValueTask<Branch> AddBranchAsync(Branch branch);
    }
}
