using System;
using System.Collections.Generic;
using System.Text;

namespace PlinxHub.Common.Contracts
{
    public interface IAuditable
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
