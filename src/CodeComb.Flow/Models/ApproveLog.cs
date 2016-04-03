using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeComb.Flow
{
    public class ApproveLog
    {
        public Guid Id { set; get; }

        public Guid NodeId { set; get; }

        /// <summary>
        /// 请求的ID
        /// </summary>
        public Guid RequestId { set; get; }

        /// <summary>
        /// 审批者ID
        /// </summary>
        public Guid ApproverId { set; get; }

        /// <summary>
        /// 审批者给的状态
        /// </summary>
        public ApproveStatus Status { set; get; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Hint { set; get; }

        public DateTime CreatedDate { get; set; }
    }
}
