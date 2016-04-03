using Microsoft.Data.Entity;

namespace CodeComb.Flow.EntityFramewrok
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SetupCodeCombFlow<TRequest>(this ModelBuilder self)
            where TRequest : Request
        {
            self.Entity<ApproveLog>(e =>
            {
                e.HasIndex(x => x.NodeId);
                e.HasIndex(x => x.RequestId);
                e.HasIndex(x => x.Status);
                e.Property(x => x.ApproverId).HasMaxLength(64);
                e.HasIndex(x => x.ApproverId);
                e.HasIndex(x => x.CreatedDate);
            });

            self.Entity<Node>(e =>
            {
                e.HasIndex(x => x.SubId);
                e.HasIndex(x => x.SubId);
            });

            self.Entity<NodeRelation>(e =>
            {
                e.HasIndex(x => x.NodeId);
                e.HasIndex(x => x.Transition);
            });

            self.Entity<TRequest>(e =>
            {
                e.HasIndex(x => x.Status);
                e.HasIndex(x => x.SubId);
                e.Property(x => x.UserId).HasMaxLength(64);
                e.HasIndex(x => x.UserId);
            });

            self.Entity<Sub>(e =>
            {
                e.HasIndex(x => x.CreatedDate);
                e.Property(x => x.Title).HasMaxLength(256);
                e.HasIndex(x => x.Title);
            });

            return self;
        }
        public static ModelBuilder SetupCodeCombFLow(this ModelBuilder self)
        {
            return self.SetupCodeCombFlow<Request>();
        }
    }
}