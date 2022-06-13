using UserManagement.DAL.IRepositories;

namespace UserManagement.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public IUserRepository UserRepository { get; set; }
}
