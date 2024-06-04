using MyTE_Migration.Areas.Admin.Models;

namespace MyTE_Migration.Areas.Admin.Service
{
    public interface IHourRepository
    {
        Task<IEnumerable<HorasTrabalhadas>> GetAllHorasTrabalhadasAsync();
        Task<HorasTrabalhadas> GetHorasTrabalhadasByIdAsync(int id);
        Task AddHorasTrabalhadsAsync(HorasTrabalhadas horasTrabalhadas);
        Task UpdateHorasTrabalhadasAsync(HorasTrabalhadas horasTrabalhadas);
        Task DeleteHorasTrabalhadasAsync(int id);
    }
}
