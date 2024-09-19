using AngularWebApi.Data;
using AngularWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AngularWebApi.Repositories
{
    public class RegionManager
    {
        private readonly AppDBContext _appDBContext;
        private readonly UserManager<IdentityUser> _userManager;
        public RegionManager(UserManager<IdentityUser> userManager, AppDBContext appDBContext) 
        {
            _userManager = userManager;
            _appDBContext = appDBContext;
        }

        public async Task<Region?> AddRegionAsync(string region)
        {
            EntityEntry<Region> addedRegion = await _appDBContext.LURegions.AddAsync(new Region() { Label = region});
            await _appDBContext.SaveChangesAsync();
            return await _appDBContext.LURegions.FindAsync(addedRegion.Entity.Id);
        }

        public IEnumerable<Region> GetRegions()
        {
            return _appDBContext.LURegions.ToList();
        }

        public async Task<bool> MapUserToRegionAsync(IdentityUser user, int regionId)
        {
            if (user.Email == null) { throw new ArgumentNullException("User with email Not available in db"); }
            IdentityUser? mapUser = await _userManager.FindByEmailAsync(user.Email);
            if(mapUser == null) { throw new ArgumentNullException("User Not available in db"); }
            EntityEntry<UserRegions> entry =  await _appDBContext.UserRegionMap.AddAsync(new UserRegions() { RegionId = regionId, UserId = mapUser.Id });
            await _appDBContext.SaveChangesAsync();
            if (entry == null) {  return false; }
            return true;
        }
    }
}
