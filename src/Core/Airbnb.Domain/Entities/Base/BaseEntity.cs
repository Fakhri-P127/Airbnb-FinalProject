using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt{ get; set; }
        // false = gizledilen, null = pending, true = gorsenen
        public bool? Status { get; set; } 

    }
}
