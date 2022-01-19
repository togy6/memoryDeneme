using memoryDeneme.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memoryDeneme.Models
{
    public class GeneralSettingsRepo : IGeneralSettingsService
    {
        //Inject Context...
        //Inject IMemoryCache...
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GeneralSettingsRepo(IMemoryCache memoryCache, ApplicationDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<GeneralSettings> GetSettingsAsync()
        {
            if (!_memoryCache.TryGetValue("GeneralSettings", out GeneralSettings GS))
            {
                GS = await _context.generalSettings.AsNoTracking().FirstOrDefaultAsync();

                _ = _memoryCache.Set<GeneralSettings>("GeneralSettings", GS, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2),
                    SlidingExpiration = TimeSpan.FromHours(2)
                });
            }
            return GS;
        }
    }
}
