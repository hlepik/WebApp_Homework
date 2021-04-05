using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Picture = Domain.App.Picture;

namespace DAL.App.EF.Repositories
{
    public class PictureRepository : BaseRepository<DAL.App.DTO.Picture, Picture, AppDbContext>,IPictureRepository
    {

        public PictureRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PictureMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Picture>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Product)
                .Where(c => c.Product!.AppUserId == userId);;


            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();


            return res!;

        }

        public  async Task<IEnumerable<DAL.App.DTO.Picture>> GetAllPicturesAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query.Include(x => x.Product);

            var resQuery = query
                .Select(p => new DAL.App.DTO.Picture()
            {
                Id = p.Id,
                Url = p.Url,
                ProductId = p.ProductId,
                ProductOwner = p.Product!.AppUserId,
                ProductName = p.Product!.Description

            }).Where(p => p.ProductOwner == userId)
                .OrderBy(x => x.ProductName);

            return await resQuery.ToListAsync();

        }

        public override async Task<DAL.App.DTO.Picture?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            query = query.Include(x => x.Product);

            var resQuery = query
                .Select(p => new DAL.App.DTO.Picture()
                {
                    Id = p.Id,
                    Url = p.Url,
                    ProductName = p.Product!.Description,
                    ProductId = p.Product.Id,
                    ProductOwner = p.Product.AppUserId

            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }
    }

}