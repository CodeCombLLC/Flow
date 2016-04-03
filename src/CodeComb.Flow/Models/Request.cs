using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeComb.Flow
{
    public class Request
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid SubId { set; get; }

        /// <summary>
        /// 发起用户的ID
        /// </summary>
        public string UserId { set; get; }
           
        public ApproveStatus Status { set; get; }
    }
}
