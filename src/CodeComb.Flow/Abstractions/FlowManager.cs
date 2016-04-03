using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeComb.Flow.Abstractions
{
    public abstract class FlowManager<TSub, TNode, TRelation, TLog, TRequest>
        where TSub : Sub
        where TNode : Node
        where TRelation : NodeRelation
        where TLog : ApproveLog
        where TRequest : Request
    {
        public IFlowStorageProvider<TSub, TNode, TRelation, TLog, TRequest> Storage { get; }

        public FlowManager(IFlowStorageProvider<TSub, TNode, TRelation, TLog, TRequest> storageProvider)
        {
            Storage = storageProvider;
        }

        public ICollection<TNode> GetCurrentStep(Guid RequestId)
        {
            var request = Storage.GetRequest(RequestId);
            var begin = Storage.GetBeginOfSub(request.SubId);
            return TraverseGraph(begin, null, RequestId);
        }

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
