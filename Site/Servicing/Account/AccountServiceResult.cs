using System.Collections.Generic;

namespace Servicing.Account
{
    public class AccountServiceResult
    {
        public bool IsSuccess { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
