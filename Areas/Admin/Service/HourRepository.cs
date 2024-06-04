using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Context;

namespace MyTE_Migration.Areas.Admin.Service
{
    public class HourRepository : IHourRepository
    {
        private readonly AppDbContext _appDbContext;

        public HourRepository(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<HorasTrabalhadas>> GetAllHorasTrabalhadasAsync()
        {
            return await _appDbContext.HorasTrabalhadas.Include(w => w.funcionario).ToListAsync();
        }

        public async Task<HorasTrabalhadas> GetHorasTrabalhadasByIdAsync(int id)
        {
            return await _appDbContext.HorasTrabalhadas.Include(w => w.funcionario).FirstOrDefaultAsync(w => w.Funcionario_ID == id);
        }

        public async Task AddWorkHourAsync(HorasTrabalhadas horasTrabalhadas)
        {
            await _appDbContext.HorasTrabalhadas.AddAsync(horasTrabalhadas);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateHorasTrabalhadasAsync(HorasTrabalhadas horasTrabalhadas)
        {
            _appDbContext.HorasTrabalhadas.Update(horasTrabalhadas);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteHorasTrabalhadasAsync(int id)
        {
            var horasTrabalhadas = await _appDbContext.HorasTrabalhadas.FindAsync(id);
            if (horasTrabalhadas != null)
            {
                _appDbContext.HorasTrabalhadas.Remove(horasTrabalhadas);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public Task AddHorasTrabalhadsAsync(HorasTrabalhadas horasTrabalhadas)
        {
            throw new NotImplementedException();
        }
    }
}
