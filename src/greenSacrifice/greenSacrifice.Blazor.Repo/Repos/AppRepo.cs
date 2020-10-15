
namespace greenSacrifice.Blazor.Repo.Repos
{
    public abstract class AppRepo<T>
    {
        protected readonly T DbContext;

        protected AppRepo(T dbContext)
        {
            DbContext = dbContext;
        }
    }
}
