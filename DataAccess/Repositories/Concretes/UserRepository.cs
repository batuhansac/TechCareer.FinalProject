using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.Entities;

namespace DataAccess.Repositories.Concretes;

public class UserRepository : EFRepositoryBase<BaseDBContext, User, Guid>, IUserRepository
{
    public UserRepository(BaseDBContext baseDBContext) : base(baseDBContext)
    {
        
    }
}
