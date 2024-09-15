namespace BackendService.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository();
        ICartRepository CartRepository();
        IProductRepository ProductRepository();
        ITransactionRepository TransactionRepository();

        void Save();
    }
}
