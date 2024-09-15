using BackendService.Repositories.DbContexts;

namespace BackendService.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        private readonly CartDbContext _cartDbContext;
        private readonly ProductDbContext _productDbContext;
        private readonly UserDbContex _userDbContex;
        private readonly TransactionDbContext _transactionDbContext;

        public UnitOfWork(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository, ITransactionRepository transactionRepository, CartDbContext cartDbContext, ProductDbContext productDbContext, UserDbContex userDbContex, TransactionDbContext transactionDbContext)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _cartDbContext = cartDbContext;
            _productDbContext = productDbContext;
            _userDbContex = userDbContex;
            _transactionDbContext = transactionDbContext;
        }

        public ICartRepository CartRepository()
        {
            return _cartRepository;
        }

        public IProductRepository ProductRepository()
        {
            return _productRepository;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public ITransactionRepository TransactionRepository()
        {
            return _transactionRepository;
        }

        public IUserRepository UserRepository()
        {
            return _userRepository;
        }


    }
}
