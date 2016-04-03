using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CodeComb.Flow;
using CodeComb.Flow.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FlowServiceCollectionExtensions
    {
        public static FlowBuilder AddCodeCombFlow(this ServiceCollection self)
        {
            return new FlowBuilder { services = self };
        }

        public static FlowBuilder AddRawFlowManager<TSub, TNode, TRelation, TLog, TRequest, TUser, TManager>(this FlowBuilder self)
            where TSub : Sub
            where TNode : Node
            where TRelation : NodeRelation
            where TLog : ApproveLog
            where TRequest : Request
            where TUser : class
            where TManager : FlowManager<TSub, TNode, TRelation, TLog, TRequest, TUser>
        {
            self.services.AddScoped<FlowManager<TSub, TNode, TRelation, TLog, TRequest, TUser>, TManager>();
            return self;
        }
    }
}
