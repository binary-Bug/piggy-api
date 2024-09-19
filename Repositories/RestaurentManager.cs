using AngularWebApi.Data;
using AngularWebApi.Migrations;
using AngularWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AngularWebApi.Repositories
{
    public class RestaurentManager
    {
        private readonly AppDBContext _appDBContext;
        public RestaurentManager(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<Restaurent?> AddRestaurentAsync(IdentityUser user, string restaurent, int restaurentTypeId)
        {
            EntityEntry<Restaurent> addedResataurent = await _appDBContext.LURestaurents.AddAsync(new Restaurent() 
            { 
                RestaurentName = restaurent,
                RestaurentType = await GetRestaurentTypeAsync(restaurentTypeId),
                RestaurentOwnerId = user.Id
            });
            await _appDBContext.SaveChangesAsync();
            return await _appDBContext.LURestaurents.FindAsync(addedResataurent.Entity.RestaurentId);
        }

        private async Task<RestaurentType?> GetRestaurentTypeAsync(int restaurentTypeId)
        {
            return await _appDBContext.LURestaurentTypes.FindAsync(restaurentTypeId);
        }

    }
}

