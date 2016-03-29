using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCombFlow
{
    public class NodeRelation
    {


        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ChildId { set; get; }

        /// <summary>
        /// Node的ID
        /// </summary>
        public Guid NodeId { set; get; }

        /// <summary>
        /// 流转
        /// </summary>
        public bool Transition { set; get; }

    }
}
