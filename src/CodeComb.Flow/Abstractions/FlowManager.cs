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
            if (request.Status != ApproveStatus.Processing)
                return Storage
                    .GetNodesBySubId(request.SubId)
                    .Where(x => x.Type == NodeType.End)
                    .ToList();
            var logs = Storage.GetLogsByRequestId(RequestId);
            var nodes = Storage.GetNodesBySubId(request.SubId);
            var begin = nodes.Where(x => x.Type == NodeType.Begin).Single();
            var hash = new Dictionary<Guid, bool>();
        }

        private ICollection<Node> TraverseGraph(Node Current, Guid RequestId, int layer = 0)
        {
            var steps = Storage.GetfSubStepLogs(RequestId, Current.Id);
            if (Current.Type == NodeType.Single)
            {

            }
            else if (Current.Type == NodeType.GroupAnd)
            {

            }
            else if (Current.Type == NodeType.GroupOr)
            {

            }
            else if (Current.Type == NodeType.And)
            {

            }
            else if (Current.Type == NodeType.Or)
            {

            }
            else if (Current.Type == NodeType.End)
            {

            }
        }
    }
}
