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
                ProductName = p.Product!.Description

            }).OrderBy(x => x.ProductName);

            return await resQuery.ToListAsync();

        }

        public  async Task<DAL.App.DTO.Picture?> FirstOrDefaultDTOAsync(Guid id, Guid userId = default, bool noTracking = true)
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


            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }
        public async Task<Guid> GetId(Guid id)
        {
            var query = RepoDbContext
                .Pictures
                .Where(x => x.ProductId == id)
                .Select(x => x.Id);

            return await query.FirstAsync();
        }
        public void RemovePictureAsync(Guid? id, Guid userId = default)
        {
            var query = CreateQuery(userId);

            query = query
                .Where(x => x.ProductId == id);

            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }
        }
    }

}