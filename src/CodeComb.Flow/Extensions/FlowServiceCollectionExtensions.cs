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
            self.AddScoped<FlowManager>();
            return new FlowBuilder { services = self };
        }
    }
}
