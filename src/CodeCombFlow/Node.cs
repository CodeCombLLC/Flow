using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCombFlow
{
    public class Node
    {
        public Guid Id { set; get; }

        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeType Type { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Approvers { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 流程的ID
        /// </summary>
        public Guid SubID { set; get; }

    }
}
