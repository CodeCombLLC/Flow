using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeComb.Flow.Identity;

namespace  Microsoft.Extensions.DependencyInjection
{
    public static class FlowBuilderExtensions
    {
        public static FlowBuilder AddIdentityUserProvider<TUser>(this FlowBuilder self)
            where TUser : class
        {
            self.services.AddScoped<IdentityUserProvider<TUser>>();
            return self;
        }
    }
}
