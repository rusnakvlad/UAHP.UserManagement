using UserManagement.DAL.IRepositories;

namespace UserManagement.DAL.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
}
