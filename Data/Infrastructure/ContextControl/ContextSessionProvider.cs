namespace DL.Data.Infrastructure.ContextControl
{
    public interface IContextSessionProvider
    {
        ApplicationDbContext ContextSession();
        void Dispose();
    }

    public class ContextSessionProvider : IContextSessionProvider
    {
        private readonly IDbContextFactory _dbContextFactory;
        private ApplicationDbContext _context;

        public ContextSessionProvider(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ApplicationDbContext ContextSession()
        {
            return _context ?? (_context = _dbContextFactory.For());
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}