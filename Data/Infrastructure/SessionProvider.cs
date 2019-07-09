namespace DL.Data.Infrastructure
{
    public interface IContextSessionProvider
    {
        ApplicationContext ContextSession();
        void Dispose();
    }

    public class ContextSessionProvider : IContextSessionProvider
    {
        private readonly IDbContextFactory _dbContextFactory;
        private ApplicationContext _context;

        public ContextSessionProvider(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ApplicationContext ContextSession()
        {
            if(_context == null)
                _context = _dbContextFactory.For();
            
            return _context;
            //return _context ?? (_context = _dbContextFactory.For());
        }

        public void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}