using Core.Persistence.Repositories;
using Models.Entities;

namespace DataAccess.Repositories.Abstracts;

public interface IUserRepository : IEntityRepository<User, Guid>
{
}
