namespace MyTE_Migration.Areas.Admin.Service
{
    public interface IWBSRepository
    {
        Task<List<string>> GetAllWbsCodesAsync();
    }
}
