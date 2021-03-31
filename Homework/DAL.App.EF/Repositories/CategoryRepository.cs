using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.Domain.Base;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using Category = Domain.App.Category;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : BaseRepository<Category, AppDbContext>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public  async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var query = CreateQuery();

            var resQuery = query.Select(p => new CategoryDTO()
                {
                    Name = p.Name,
                    Id = p.Id
                })
                .OrderBy(x => x.Name);

            return await resQuery.ToListAsync();

        }



    }
}