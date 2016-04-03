namespace CodeComb.Flow.Abstractions
{
    public interface IFlowUserProvider<TUser>
        where TUser : class
    {
        TUser GetUser(string userId);
    }
}
