using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeComb.Flow;
using CodeComb.Flow.EntityFramewrok;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FlowBuilderExtensions
    {
        public static FlowBuilder AddEntityFrameworkStorage<TContext, TRequest>(this FlowBuilder self)
            where TContext : class, IFlowDbContext<TRequest>
            where TRequest : Request
        {
            self.services.AddScoped<IFlowDbContext<TRequest>, TContext>();
            return self;
        }

        public static FlowBuilder AddEntityFrameworkStorage<TContext>(this FlowBuilder self)
            where TContext : class, IFlowDbContext<Request>
        {
            self.AddEntityFrameworkStorage<TContext, Request>();
            return self;
        }
    }
}
