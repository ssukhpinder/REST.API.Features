using Microsoft.EntityFrameworkCore;
using REST.API.EFCore.Context;
using REST.API.EFCore.Interface;
using REST.API.EFCore.Models;

namespace REST.API.EFCore.Services
{
    public class CommonService(EFCoreDbContext efCoreDbContext) : ICommonService
    {
        private readonly EFCoreDbContext _efCoreDbContext = efCoreDbContext;

        public async Task<List<SampleTable>> GetSampleTables()
        {
            return await _efCoreDbContext.SampleTables.ToListAsync();
        }
    }
}
