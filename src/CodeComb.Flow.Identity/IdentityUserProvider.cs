using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using CodeComb.Flow.Abstractions;

namespace CodeComb.Flow.Identity
{
    public class IdentityUserProvider<TUser> : IFlowUserProvider<TUser>
        where TUser : class
    {
        private UserManager<TUser> UM;

        public IdentityUserProvider(UserManager<TUser> userManager)
        {
            UM = userManager;
        }

        public TUser GetUser(string userId)
        {
            var task = UM.FindByIdAsync(userId);
            task.Wait();
            return task.Result;
        }
    }
}
