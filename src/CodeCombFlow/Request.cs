using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCombFlow
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

        /// <summary>
        /// 当前走在哪个ID
        /// </summary>
        public Guid CurrentNodeId { set; get; }
           
         
        public ApproveStatus Status { set; get; }
       
    }
}
