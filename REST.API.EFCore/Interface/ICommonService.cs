using REST.API.EFCore.Models;

namespace REST.API.EFCore.Interface
{
    public interface ICommonService
    {
        public Task<List<SampleTable>> GetSampleTables();
    }
}
