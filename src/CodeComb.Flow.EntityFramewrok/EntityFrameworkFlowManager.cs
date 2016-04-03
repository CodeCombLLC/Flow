using CodeComb.Flow.Abstractions;

namespace CodeComb.Flow.EntityFramewrok
{
    public class EntityFrameworkFlowManager<TUser, TRequest> : FlowManager<Sub, Node, NodeRelation, ApproveLog, TRequest, TUser>
        where TRequest : Request
        where TUser : class
    {
        public EntityFrameworkFlowManager(IFlowStorageProvider<Sub, Node, NodeRelation, ApproveLog, TRequest> storageProvider, IFlowUserProvider<TUser> userProvider)
            : base(storageProvider, userProvider)
        {
        }
    }
}
