using System;
using System.Collections.Generic;

namespace CodeComb.Flow.Abstractions
{
    public interface IFlowStorageProvider<TSub, TNode, TRelation, TLog, TRequest>
        where TSub : Sub
        where TNode : Node
        where TRelation : NodeRelation
        where TLog : ApproveLog
        where TRequest : Request
    {
        TSub GetSub(Guid id);
        void RemoveSub(Guid id);
        void UpdateSub(Guid id, TSub sub);
        void CreateSub(TSub sub);

        TNode GetNode(Guid id);
        void RemoveNode(Guid id);
        void UpdateNode(Guid id, TNode node);
        void CreateNode(TNode node);
        ICollection<TNode> GetNodesBySubId(Guid SubId);
        ICollection<TNode> GetNextNodes(Guid NodeId);
        ICollection<TNode> GetPrevNodes(Guid NodeId);

        bool RelationExist(Guid NodeId, Guid ChildId, bool Transition);
        void RemoveRelation(Guid NodeId, Guid ChildId, bool Transition);
        void CreateRelation(Guid NodeId, Guid ChildId, bool Transition);

        TRequest GetRequest(Guid id);
        void RemoveRequest(Guid id);
        void UpdateRequest(Guid id, TRequest request);
        void CreateRequest(TRequest request);

        TLog GetLog(Guid id);
        void RemoveLog(Guid id);
        void UpdateLog(Guid id, TLog log);
        void CreateLog(TLog log);
        ICollection<TLog> GetLogsByRequestId(Guid RequestId);
    }
}
