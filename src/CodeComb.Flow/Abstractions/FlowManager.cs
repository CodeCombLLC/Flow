using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeComb.Flow.Abstractions
{
    public abstract class FlowManager<TSub, TNode, TRelation, TLog, TRequest, TUser>
        where TSub : Sub
        where TNode : Node
        where TRelation : NodeRelation
        where TLog : ApproveLog
        where TRequest : Request
        where TUser : class
    {
        public IFlowStorageProvider<TSub, TNode, TRelation, TLog, TRequest> Storage { get; }
        private IFlowUserProvider<TUser> UP;

        public FlowManager(IFlowStorageProvider<TSub, TNode, TRelation, TLog, TRequest> storageProvider, IFlowUserProvider<TUser> userProvider)
        {
            Storage = storageProvider;
            UP = userProvider;
        }

        /// <summary>
        /// 获取当前过程阶段
        /// </summary>
        /// <param name="RequestId"></param>
        /// <returns></returns>
        public virtual ICollection<TNode> GetCurrentStep(Guid RequestId)
        {
            var request = Storage.GetRequest(RequestId);
            var begin = Storage.GetBeginOfSub(request.SubId);
            return TraverseGraph(begin, null, RequestId);
        }

        /// <summary>
        /// 获取当前过程未进行审批的用户ID
        /// </summary>
        /// <param name="RequestId"></param>
        /// <returns></returns>
        public virtual ICollection<string> GetCurrentApproverIds(Guid RequestId)
        {
            var nodes = GetCurrentStep(RequestId);
            return nodes
                .SelectMany(x => Storage.GetfSubStepLogs(RequestId, x.Id))
                .Where(x => x.Status == ApproveStatus.Processing)
                .Select(x => x.ApproverId)
                .ToList();
        }

        /// <summary>
        /// 获取当前过程未进行审批的用户
        /// </summary>
        /// <param name="RequestId"></param>
        /// <returns></returns>
        public virtual ICollection<TUser> GetCurrentApprovers(Guid RequestId)
        {
            return GetCurrentApproverIds(RequestId)
                .Select(x => UP.GetUser(x))
                .ToList();
        }

        /// <summary>
        /// 遍历过程图结构
        /// </summary>
        /// <param name="Current"></param>
        /// <param name="Prev"></param>
        /// <param name="RequestId"></param>
        /// <returns></returns>
        protected virtual ICollection<TNode> TraverseGraph(TNode Current, TNode Prev, Guid RequestId)
        {
            var steps = Storage.GetfSubStepLogs(RequestId, Current.Id);
            if (Current.Type == NodeType.Single)
            {
                var logs = Storage.GetfSubStepLogs(RequestId, Current.Id);
                if (logs.Any(x => x.Status == ApproveStatus.Processing))
                {
                    return new List<TNode> { Current };
                }
                else
                {
                    var next = Storage.GetNextNodes(Current.Id);
                    return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
                }
            }
            else if (Current.Type == NodeType.GroupAnd)
            {
                var logs = Storage.GetfSubStepLogs(RequestId, Current.Id);
                if (logs.Any(x => x.Status == ApproveStatus.Processing))
                {
                    return Storage.GetNextNodes(Prev.Id).ToList();
                }
                else
                {
                    var next = Storage.GetNextNodes(Current.Id);
                    return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
                }
            }
            else if (Current.Type == NodeType.GroupOr)
            {
                var logs = Storage.GetfSubStepLogs(RequestId, Current.Id);
                if (logs.All(x => x.Status == ApproveStatus.Processing))
                {
                    return Storage.GetNextNodes(Prev.Id).ToList();
                }
                else
                {
                    var next = Storage.GetNextNodes(Current.Id);
                    return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
                }
            }
            else if (Current.Type == NodeType.And)
            {
                var prev = Storage.GetPrevNodes(Current.Id);
                var logs = prev.SelectMany(x => Storage.GetfSubStepLogs(RequestId, x.Id)).ToList();
                if (logs.Any(x => x.Status == ApproveStatus.Processing))
                {
                    return prev;
                }
                else
                {
                    var next = Storage.GetNextNodes(Current.Id);
                    return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
                }
            }
            else if (Current.Type == NodeType.Or)
            {
                var prev = Storage.GetPrevNodes(Current.Id);
                var logs = prev.SelectMany(x => Storage.GetfSubStepLogs(RequestId, x.Id)).ToList();
                if (logs.All(x => x.Status == ApproveStatus.Processing))
                {
                    return prev;
                }
                else
                {
                    var next = Storage.GetNextNodes(Current.Id);
                    return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
                }
            }
            else if (Current.Type == NodeType.End)
            {
                return new List<TNode> { Current };
            }
            else // Begin
            {
                var next = Storage.GetNextNodes(Current.Id);
                return next.SelectMany(x => TraverseGraph(x, Current, RequestId)).ToList();
            }
        }
    }
}
