using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.Entities;

namespace DataAccess.Repositories.Concretes;

public class CategoryRepository : EFRepositoryBase<BaseDBContext, Category, int>, ICategoryRepository
{
    public CategoryRepository(BaseDBContext baseDBContext) : base(baseDBContext)
    {
        
    }
}
