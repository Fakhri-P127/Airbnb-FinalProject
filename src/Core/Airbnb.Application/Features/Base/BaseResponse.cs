using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Contracts.v1.Base
{
    public class BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; } 
        //banned suspended or active
        public bool? Status { get; set; } = true;

    }
}
