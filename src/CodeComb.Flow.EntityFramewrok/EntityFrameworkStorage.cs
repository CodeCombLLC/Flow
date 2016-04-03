using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CodeComb.Flow.Abstractions;

namespace CodeComb.Flow.EntityFramewrok
{
    public class EntityFrameworkStorage<TRequest> : IFlowStorageProvider<Sub, Node, NodeRelation, ApproveLog, TRequest>
        where TRequest : Request
    {
        private IFlowDbContext<TRequest> DB;

        public EntityFrameworkStorage(IServiceProvider services, IFlowDbContext<TRequest> DbContext)
        {
            DB = (IFlowDbContext<TRequest>)services.GetRequiredService(DbContext.GetType());
        }

        public void CreateLog(ApproveLog log)
        {
            DB.ApproveLogs.Add(log);
            DB.SaveChanges();
        }

        public void CreateNode(Node node)
        {
            DB.Nodes.Add(node);
            DB.SaveChanges();
        }

        public void CreateRelation(NodeRelation relation)
        {
            DB.NodeRelations.Add(relation);
            DB.SaveChanges();
        }

        public void CreateRelation(Guid NodeId, Guid ChildId, bool Transition)
        {
            throw new NotImplementedException();
        }

        public void CreateRequest(TRequest request)
        {
            DB.Requests.Add(request);
            DB.SaveChanges();
        }

        public void CreateSub(Sub sub)
        {
            DB.Subs.Add(sub);
            DB.SaveChanges();
        }

        public ICollection<ApproveLog> GetfSubStepLogs(Guid RequestId, Guid NodeId)
        {
            return DB.ApproveLogs.Where(x => x.RequestId == RequestId && x.NodeId == NodeId).ToList();
        }

        public ApproveLog GetLog(Guid id)
        {
            return DB.ApproveLogs.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<ApproveLog> GetLogsByRequestId(Guid RequestId)
        {
            return DB.ApproveLogs
                .Where(x => x.RequestId == RequestId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public ICollection<Node> GetNextNodes(Guid NodeId)
        {
            var nodeIds = DB.NodeRelations
                .Where(x => x.NodeId == NodeId)
                .Select(x => x.ChildId)
                .ToList();
            return DB.Nodes
                .Where(x => nodeIds.Contains(x.Id))
                .ToList();
        }

        public Node GetNode(Guid id)
        {
            return DB.Nodes.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<Node> GetNodesBySubId(Guid SubId)
        {
            return DB.Nodes
                .Where(x => x.SubId == SubId)
                .ToList();
        }

        public ICollection<Node> GetPrevNodes(Guid NodeId)
        {
            var nodeIds = DB.NodeRelations
                .Where(x => x.ChildId == NodeId)
                .Select(x => x.NodeId)
                .ToList();
            return DB.Nodes
                .Where(x => nodeIds.Contains(x.Id))
                .ToList();
        }
        
        public TRequest GetRequest(Guid id)
        {
            return DB.Requests.SingleOrDefault(x => x.Id == id);
        }

        public Sub GetSub(Guid id)
        {
            return DB.Subs.SingleOrDefault(x => x.Id == id);
        }

        public bool RelationExist(Guid NodeId, Guid ChildId, bool Transition)
        {
            var r = DB.NodeRelations.SingleOrDefault(x => x.NodeId == NodeId && x.ChildId == ChildId && x.Transition == Transition);
            return r != null;
        }

        public void RemoveLog(Guid id)
        {
            var log = DB.ApproveLogs.SingleOrDefault(x => x.Id == id);
            if (log != null)
            {
                DB.ApproveLogs.Remove(log);
                DB.SaveChanges();
            }
        }

        public void RemoveNode(Guid id)
        {
            var node = DB.Nodes.Single(x => x.Id == id);
            if (node != null)
            {
                DB.Nodes.Remove(node);
                DB.SaveChanges();
            }
        }

        public void RemoveRelation(Guid NodeId, Guid ChildId, bool Transition)
        {
            if (RelationExist(NodeId, ChildId, Transition))
            {
                var r = DB.NodeRelations.Single(x => x.NodeId == NodeId && x.ChildId == ChildId && x.Transition == Transition);
                DB.NodeRelations.Remove(r);
                DB.SaveChanges();
            }
        }

        public void RemoveRequest(Guid id)
        {
            var request = DB.Requests.SingleOrDefault(x => x.Id == id);
            if (request != null)
            {
                DB.Requests.Remove(request);
                DB.SaveChanges();
            }
        }

        public void RemoveSub(Guid id)
        {
            var sub = DB.Subs.SingleOrDefault(x => x.Id == id);
            if (sub != null)
            {
                DB.Subs.Remove(sub);
                DB.SaveChanges();
            }
        }

        public void UpdateLog(Guid id, ApproveLog Log)
        {
            var log = DB.ApproveLogs.SingleOrDefault(x => x.Id == id);
            if (log != null)
                DB.ApproveLogs.Remove(log);
            Log.Id = id;
            DB.ApproveLogs.Add(Log);
            DB.SaveChanges();
        }

        public void UpdateNode(Guid id, Node Node)
        {
            var node = DB.Nodes.SingleOrDefault(x => x.Id == id);
            if (node != null)
                DB.Nodes.Remove(node);
            Node.Id = id;
            DB.Nodes.Add(Node);
            DB.SaveChanges();
        }
        
        public void UpdateRequest(Guid id, TRequest request)
        {
            var req = DB.Requests.SingleOrDefault(x => x.Id == id);
            if (req != null)
                DB.Requests.Remove(req);
            request.Id = id;
            DB.Requests.Add(request);
            DB.SaveChanges();
        }

        public void UpdateSub(Guid id, Sub Sub)
        {
            var sub = DB.Subs.SingleOrDefault(x => x.Id == id);
            if (sub != null)
                DB.Subs.Remove(sub);
            Sub.Id = id;
            DB.Subs.Add(Sub);
            DB.SaveChanges();
        }
    }
}
