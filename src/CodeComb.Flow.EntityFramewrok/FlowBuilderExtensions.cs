using CodeComb.Flow;
using CodeComb.Flow.EntityFramewrok;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FlowBuilderExtensions
    {
        public static FlowBuilder AddEntityFrameworkStorage<TContext, TUser, TRequest>(this FlowBuilder self)
            where TContext : class, IFlowDbContext<TRequest>
            where TRequest : Request
            where TUser : class
        {
            self.services.AddScoped<IFlowDbContext<TRequest>, TContext>();
            self.AddRawFlowManager<Sub, Node, NodeRelation, ApproveLog, TRequest, TUser, EntityFrameworkFlowManager<TUser, TRequest>>();
            return self;
        }

        public static FlowBuilder AddEntityFrameworkStorage<TContext, TUser>(this FlowBuilder self)
            where TContext : class, IFlowDbContext<Request>
            where TUser : class
        {
            self.AddEntityFrameworkStorage<TContext, TUser, Request>();
            return self;
        }
    }
}