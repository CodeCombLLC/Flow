using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace CodeComb.Flow.EntityFramewrok
{
    public interface IFlowDbContext<TRequest>
        where TRequest : Request
    {
        DbSet<ApproveLog> ApproveLogs { get; set; }
        DbSet<Node> Nodes { get; set; }
        DbSet<NodeRelation> NodeRelations { get; set; }
        DbSet<TRequest> Requests { get; set; }
        DbSet<Sub> Subs { get; set; }
        int SaveChanges();
    }
}
