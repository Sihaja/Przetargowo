using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przetargi.Models
{
    public enum ParticipateResult
    {
        None = 0,
        Success = 1,
        FailureCantParticipateTwice = 2,
        FailureTenderDoesNotExist = 3,
        FailureInternalError = 4,
        FailureNoDocument = 5,
    }
}